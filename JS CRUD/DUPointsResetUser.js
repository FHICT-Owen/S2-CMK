const botconfig = require(`../botconfig.json`);
const db = require(`../utils/db.js`);

module.exports = {
	name: `points-reset`,
	description: `Check the points for a certain user.`,
	usage: `${botconfig.prefix}points-reset`,
	aliases: [`pr`],
	allowedChannels: [botconfig.DUBotChannelID],

	execute(message, args) {
		const { guild } = message;
		if (botconfig.adminRoles.some(role => guild.members.cache.get(message.author.id).roles.cache.has(role)) || botconfig.DUPointsRoles.some(role => guild.members.cache.get(message.author.id).roles.cache.has(role))) {
			const DUUser = message.mentions.users.first() || message.guild.members.cache.get(args[0]);
			if(!DUUser) return message.channel.send(`Could not find the specified user.`);
			
			db.query(`SELECT * FROM points WHERE userid = '${DUUser.id}'`, (err) => {
				if(err) throw err;
				let sql;
				const reset = 0;
				// eslint-disable-next-line prefer-const
				sql = `UPDATE points SET pointsvalue = ${reset} WHERE userid = '${DUUser.id}'`;
				message.channel.send(`${DUUser}'s points have now been reset.`);
				db.query(sql);
			});
		} else message.channel.send(`Insufficient permissions to use this command!`);
	}
};