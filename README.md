# LeaderboardAPI

线上机试

LeaderboardAPI 环境 .NET 6.0

运行LeaderboardAPI project
测试地址：http://localhost:5024/

Update score:
Post
http://localhost:5024/Customer/{id}/score/{score}

Get score:
Get
http://localhost:5024/Leaderboard?start={start}&end={end}

http://localhost:5024/Leaderboard/{id}?low={low}&high={high}
