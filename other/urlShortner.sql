

CREATE DATABASE UrlShortner;
use UrlShortner;



CREATE TABLE users (

	
	id int IDENTITY(1,1),
	userId varchar(10) primary key,
	firstName varchar(50) not null,
	lastName varchar(50),
	email varchar(50) not null unique,
	userType varchar(10) default 'regular'
);


insert into users(userId, firstName, lastName, email, passwrd) values('abc', 'chandrakant', 'shinde', 'cs@chandrakant.dev', 'pass-1');
insert into urls(userId, originalUrl, shortUrl) values('abc', 'chandrakant.dev', 'shortid')



alter table users add passwrd varchar(20) not null;

EXEC sp_rename 'dbo.Urls.lastModified', 'expiryDate', 'COLUMN';

create table Urls(

	id int IDENTITY(1,1) primary key,
	userId varchar(10) not null,
	originalUrl varchar(300) not null,
	shortUrl varchar(10) not null unique,
	createdAt datetime default CURRENT_TIMESTAMP,
	lastModified datetime default CURRENT_TIMESTAMP+5,
	updatedAt datetime default null,
	FOREIGN KEY (userId) references users(userId)
);


select * from users;
select * from urls;



select * from urls where shorturl = 'psvnlpIH';

