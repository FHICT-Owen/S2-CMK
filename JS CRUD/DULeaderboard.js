const botconfig = require(`../botconfig.json`);
const db = require(`../utils/db.js`);

const updateLeaderboard = async (bot) => {
	const channelId = botconfig.DULeaderboardChannelID;
	const guild = bot.guilds.cache.get(botconfig.guildID);

	const fetchTopMembers = new Promise((resolve) => {
		let text = `Here are the current DU RWI members with the most amount of points:\n\n`;
	
		db.query(`SELECT * FROM points ORDER BY pointsvalue DESC LIMIT 20`, (err, results) => {
	
			for (let counter = 0; counter < results.rows.length; ++counter) {
				const userid = results.rows[counter].userid;
				const pointsvalue = results.rows[counter].pointsvalue;
				text += `#${counter + 1} <@${userid}> has ${pointsvalue} points\n`;
				if(counter === results.rows.length - 1) {
					text += `\nThis is updated every 15 minutes.`;
					resolve(text);
				}
			}
		});
	});

	if (guild) {
		const channel = guild.channels.cache.get(channelId);
		if (channel) {
			const messages = await channel.messages.fetch();

			const firstMessage = messages.first();
			fetchTopMembers.then((message) => {
				if (firstMessage) firstMessage.edit(message);
				else channel.send(message);
			}).catch((err) => {
				console.log(err);
			});
		}
	}
};

module.exports = {
	enabled: true,
	execute(bot) {
		updateLeaderboard(bot);
	}
};