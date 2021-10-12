const botconfig = require(`../botconfig.json`);
const db = require(`../utils/db.js`);

module.exports = {
	name: `points-subtract`,
	description: `Subtract points from a certain user.`,
	usage: `${botconfig.prefix}points-subtract`,
	aliases: [`ps`],
	allowedChannels: [botconfig.DUBotChannelID],

	execute(message, args) {
		const { guild } = message;
		if (botconfig.adminRoles.some(role => guild.members.cache.get(message.author.id).roles.cache.has(role)) || botconfig.DUPointsRoles.some(role => guild.members.cache.get(message.author.id).roles.cache.has(role))) {
			const DUUser = message.mentions.users.first() || message.guild.members.cache.get(args[0]);
			if(!DUUser) return message.channel.send(`Could not find the specified user.`);
			const pointsToSubtract = parseInt(args[1], 10);
			if(!pointsToSubtract) return message.reply(`You didn't tell me how many points to subtract...`);
			
			db.query(`SELECT * FROM points WHERE userid = '${DUUser.id}'`, (err, res) => {
				if(err) throw err;
				let sql;
				const previousPoints = res.rows[0].pointsvalue;
				// eslint-disable-next-line prefer-const
				sql = `UPDATE points SET pointsvalue = ${previousPoints - pointsToSubtract} WHERE userid = '${DUUser.id}'`;
				message.channel.send(`${DUUser} got **${pointsToSubtract}** points subtracted and now has a balance of **${previousPoints - pointsToSubtract}** points.`);
				db.query(sql);
			});
		} else message.channel.send(`Insufficient permissions to use this command!`);
	}
};