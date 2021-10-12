const botconfig = require(`../botconfig.json`);
const db = require(`../utils/db.js`);

module.exports = {
	name: `points-add`,
	description: `Add points to a certain user`,
	usage: `${botconfig.prefix}points-add`,
	aliases: [`pa`],
	allowedChannels: [botconfig.DUBotChannelID],
	
	execute(message, args) {
		const { guild } = message;
		if (botconfig.adminRoles.some(role => guild.members.cache.get(message.author.id).roles.cache.has(role)) || botconfig.DUPointsRoles.some(role => guild.members.cache.get(message.author.id).roles.cache.has(role))) {
			const DUUser = message.mentions.users.first() || message.guild.members.cache.get(args[0]);
			if(!DUUser) return message.channel.send(`Could not find the specified user.`);
			const pointsToAdd = parseInt(args[1], 10);
			if(!pointsToAdd) return message.reply(`You didn't tell me how many points to add...`);
			
			db.query(`SELECT * FROM points WHERE userid = '${DUUser.id}'`, (err, res) => {
				if(err) throw err;
				let sql;
				if (res.rows.length < 1){
					sql = `INSERT INTO points(userid, pointsvalue) VALUES('${DUUser.id}', '${pointsToAdd}')`;
					message.channel.send(`${DUUser}'s entry has been created and received **${pointsToAdd}** points`);
				} else {
					const previousPoints = res.rows[0].pointsvalue;
					sql = `UPDATE points SET pointsvalue = ${previousPoints + pointsToAdd} WHERE userid = '${DUUser.id}'`;
					message.channel.send(`${DUUser} has received **${pointsToAdd}** points and now has a balance of **${previousPoints + pointsToAdd}** points.`);
				}
				
				db.query(sql);
			});
		} else message.channel.send(`Insufficient permissions to use this command!`);
	}
};