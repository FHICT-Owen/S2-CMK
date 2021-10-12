const botconfig = require(`../botconfig.json`);
const db = require(`../utils/db.js`);

module.exports = {
	name: `points-cleanup`,
	description: `Removes all entries from database that are 0 or lower`,
	usage: `${botconfig.prefix}points-cleanup`,
	aliases: [`pc`],
	allowedChannels: [botconfig.DUBotChannelID],
	
	execute(message, args) {
		const { guild } = message;
		// eslint-disable-next-line curly
		if(botconfig.adminRoles.some(role => guild.members.cache.get(message.author.id).roles.cache.has(role)) || botconfig.DUPointsRoles.some(role => guild.members.cache.get(message.author.id).roles.cache.has(role))) {
			db.query(`SELECT * FROM points WHERE pointsvalue <= 0`, (err, res) => {

				if(err) throw err;
				const usercount = res.rows.length;
				console.log(usercount);
				for (let counter = 0; counter < usercount; ++counter) {
					const userId = res.rows[counter].userid;
					db.query(`DELETE FROM points WHERE userid = '${userId}'`);
					
					if(counter === res.rows.length - 1) message.channel.send(`Removed **${usercount}** entries from the database.`);			
				}				
			});	
		}
	}	
};