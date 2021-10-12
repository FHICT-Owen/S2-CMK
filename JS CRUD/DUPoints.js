const botconfig = require(`../botconfig.json`);
const db = require(`../utils/db.js`);

module.exports = {
	name: `points`,
	description: `Check the points for a certain user.`,
	usage: `${botconfig.prefix}points`,
	aliases: [`p`],
	allowedChannels: [botconfig.DUBotChannelID],
	
	execute(message, args) {
		const DUUser = message.mentions.users.first() || message.guild.members.cache.get(args[0]) || message.author;
		if(!DUUser) return message.channel.send(`Could not find the specified user.`);

		db.query(`SELECT * FROM points WHERE userid = '${DUUser.id}'`, (err, res) => {
			if(err) throw err;

			try {
				const userPoints = res.rows[0].pointsvalue;
				message.channel.send(`${DUUser} has a balance of **${userPoints}** points.`);
			} catch (err) {
				message.channel.send(`Could not find database entry for the specified user!`);
			}
			
		});	
	}	
};