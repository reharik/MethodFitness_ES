SET ORIGINAL=%CD%
cd c:\databases\Mongo
mongod --config C:\databases\Mongo\mongo.config --install
timeout /t 5
net start MongoDB
cd %original%