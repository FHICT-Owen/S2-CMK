const botconfig = require(`../botconfig.json`);
const db = require(`../utils/db.js`);

module.exports = {
	name: `points-removeuse`,
	description: `Removes the entire entry of the specified user.`,
	usage: `${botconfig.prefix}points-removeuser`,
	aliases: [`pru`],
	allowedChannels: [botconfig.DUBotChannelID],
	
	execute(message, args) {
		const { guild } = message;
		if (botconfig.adminRoles.some(role => guild.members.cache.get(message.author.id).roles.cache.has(role)) || botconfig.DUPointsRoles.some(role => guild.members.cache.get(message.author.id).roles.cache.has(role))) {
			const DUUser = message.mentions.users.first() || message.guild.members.cache.get(args[0]);
			if(!DUUser) return message.channel.send(`Could not find the specified user.`);

			db.query(`SELECT * FROM points WHERE userid = '${DUUser.id}'`, (err) => {
				if(err) throw err;
				let sql;
				// eslint-disable-next-line prefer-const
				sql = `DELETE FROM points WHERE userid = '${DUUser.id}'`;
				message.channel.send(`${DUUser}'s entry has been removed from the database.`);
				db.query(sql);
			});
		} else message.channel.send(`Insufficient permissions to use this command!`);
	}
};