drop database if exists shop3;
create database shop3 default character set utf8 collate utf8_general_ci;
drop user if exists 'staff'@'localhost';
create user 'staff'@'localhost' identified by 'password';
grant all on shop3.* to 'staff'@'localhost';
use shop3;

create table product (
	id int auto_increment primary key, 
	name varchar(200) not null, 
	genre varchar(11) not null,
	price int not null,
	stock int not null
);
create table employee (

	id int auto_increment primary key,
	name varchar(20) not null,
	age int (3) not null,
	post varchar(5) not null,
	address varchar(200) not null
);

insert into product values(null, '松の実', '実', 700,10);
insert into product values(null, 'くるみ', '種', 270,10);
insert into product values(null, 'ひまわりの種','種', 210,10);
insert into product values(null, 'アーモンド','種', 220,10);
insert into product values(null, 'カシューナッツ','種', 250,10);
insert into product values(null, 'ジャイアントコーン','実', 180,10);
insert into product values(null, 'ピスタチオ','種', 310,10);
insert into product values(null, 'マカダミアナッツ','実', 600,10);
insert into product values(null, 'かぼちゃの種','種', 180,10);
insert into product values(null, 'ピーナッツ','種',  150,10);
insert into product values(null, 'クコの実','実', 400,10);

insert into employee values(null, '熊木 和夫',31,'一般社員', '東京都新宿区西新宿2-8-1');
insert into employee values(null, '鳥居 健二',32,'係長', '神奈川県横浜市中区日本大通1');
insert into employee values(null, '鷺沼 美子',34,'課長', '大阪府大阪市中央区大手前2');
insert into employee values(null, '鷲尾 史郎',38,'部長', '愛知県名古屋市中区三の丸3-1-2');
insert into employee values(null, '牛島 大悟',32,'専務', '埼玉県さいたま市浦和区高砂3-15-1');
insert into employee values(null, '相馬 助六', 37,'秘書','千葉県地足中央区市場町1-1');
insert into employee values(null, '猿飛 菜々子',39,'主任', '兵庫県神戸市中央区下山手通5-10-1');
insert into employee values(null, '犬山 陣八', 30,'一般社員','北海道札幌市中央区北3西6');
insert into employee values(null, '猪口 一休',33,'一般社員', '福岡県福岡市博多区東公園7-7');
