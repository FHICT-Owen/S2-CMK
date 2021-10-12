const { Pool } = require(`pg`);
const connectionString = `${process.env.DATABASE_URL}`;

//define SQL database for DU points
const pool = new Pool({
	connectionString,
	ssl: {
		rejectUnauthorized: false
	}
});

//connect to PostgreSQL DB
pool.connect(err => {
	if(err) throw err; 
	console.log(`Connected to PostgreSQL`);
});

module.exports = {
	query: (text, params) => {
		const start = Date.now();
		return pool.query(text, params, () => {
			const duration = Date.now() - start;
			console.log(`executed query: "${text}" in ${duration} miliseconds`);
		});
	},
	getClient: (callback) => {
		pool.connect((err, client, done) => {
			const query = client.query;
			// monkey patch the query method to keep track of the last query executed
			client.query = (...args) => {
				client.lastQuery = args;
				return query.apply(client, args);
			};
			// set a timeout of 5 seconds, after which we will log this client's last query
			const timeout = setTimeout(() => {
				console.error(`A client has been checked out for more than 5 seconds!`);
				console.error(`The last executed query on this client was: ${client.lastQuery}`);
			}, 5000);
			const release = (err) => {
				// call the actual 'done' method, returning this client to the pool
				done(err);
				// clear our timeout
				clearTimeout(timeout);
				// set the query method back to its old un-monkey-patched version
				client.query = query;
			};
			callback(err, client, release);
		});
	}
};