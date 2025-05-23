drop database if exists shop;
create datebase shop default character set utf8 collate utf8_general_ci;
drop user if exists 'staff'@'localhost';
create user 'staff'@'localhost' identified by 'password';
grant all on shop.* to 'staff'@'localhost';
use shop;

create table product (
id int aute_increment primary key,
name varchar(200) not null,
price int not null

);

insert into product values(null,'松の実', 700);
insert into product values(null,'くるみ', 270);
insert into product values(null,'ひまわりの種', 210);
insert into product values(null,'アーモンド', 220);
insert into product values(null,'カシューナッツ', 250);
insert into product values(null,'ジャイアントコーン', 180);
insert into product values(null,'ピスタチオ', 310);
insert into product values(null,'マカダミアナッツ', 600);
insert into product values(null,'かぼちゃの種', 180);
insert into product values(null,'ピーナッツ', 150);
insert into product values(null,'クコの実', 400);