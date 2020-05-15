--@(#) script.ddl

CREATE TABLE customer_tables
(
	id bigserial NOT NULL primary key,
	seats_count integer NOT NULL,
	qr_code varchar (255) NULL,
	is_taken boolean NOT NULL,
	join_code varchar (255) NULL
);

CREATE TABLE discounts
(
	id bigserial NOT NULL primary key,
	discount_code varchar (255) NOT NULL,
	stand_until timestamp NULL,
	discount_proc integer NOT NULL
);

CREATE TABLE dish_categories
(
	id bigserial NOT NULL primary key,
	title varchar (255) NOT NULL
);

CREATE TABLE ingredients
(
	id bigserial NOT NULL primary key,
	title varchar (255) NOT NULL
);

CREATE TABLE menus
(
	id bigserial NOT NULL primary key,
	title varchar (255) NOT NULL,
	mon boolean NOT NULL,
	tue boolean NOT NULL,
	wed boolean NOT NULL,
	thu boolean NOT NULL,
	fri boolean NOT NULL,
	sat boolean NOT NULL,
	sun boolean NOT NULL,
	time_from time NULL,
	time_until time NULL,
	date_from date NULL,
	date_until date NULL,
	is_active boolean NOT NULL
);

CREATE TABLE event_type
(
	id bigserial NOT NULL primary key,
	name char (12) NOT NULL
);
INSERT INTO event_type(id, name) VALUES(1, 'bill_request');
INSERT INTO event_type(id, name) VALUES(2, 'cancel_order');
INSERT INTO event_type(id, name) VALUES(3, 'new_customer');
INSERT INTO event_type(id, name) VALUES(4, 'new_order');

CREATE TABLE user_role
(
	id bigserial NOT NULL primary key,
	name char (13) NOT NULL
);
INSERT INTO user_role(id, name) VALUES(1, 'administrator');
INSERT INTO user_role(id, name) VALUES(2, 'waiter');

CREATE TABLE bills
(
	id bigserial NOT NULL primary key,
	date_time timestamp NULL DEFAULT CURRENT_DATE,
	tips double precision NULL,
	amount double precision NOT NULL,
	is_paid boolean NOT NULL,
	evaluation varchar (255) NOT NULL,
	fk_discounts bigint NULL,
	CONSTRAINT fkc_discounts FOREIGN KEY(fk_discounts) REFERENCES discounts (id)
);

CREATE TABLE dishes
(
	id bigserial NOT NULL primary key,
	title varchar (255) NOT NULL,
	description varchar (255) NULL,
	price double precision NOT NULL,
	calories integer NULL,
	discount double precision NULL,
	fk_dish_categories bigint NULL,
	CONSTRAINT fkc_dish_categories FOREIGN KEY(fk_dish_categories) REFERENCES dish_categories (id)
);

CREATE TABLE registered_users
(
	id bigserial NOT NULL primary key,
	name varchar (255) NOT NULL,
	surname varchar (255) NOT NULL,
	password varchar (255) NOT NULL,
	phone varchar (255) NULL,
	email varchar (255) NULL,
	birth_date date NOT NULL,
	is_blocked boolean NOT NULL,
	role bigint NOT NULL,
	FOREIGN KEY(role) REFERENCES user_role (id)
);

CREATE TABLE dish_ingredients
(
	quantity double precision NOT NULL,
	fk_dishes bigint NOT NULL,
	fk_ingredients bigint NOT NULL,
	PRIMARY KEY(fk_dishes, fk_ingredients),
	CONSTRAINT fkc_dishes FOREIGN KEY(fk_dishes) REFERENCES dishes (id),
	CONSTRAINT fkc_ingredients FOREIGN KEY(fk_ingredients) REFERENCES ingredients (id)
);

CREATE TABLE menu_dishes
(
	date_from timestamp NULL,
	date_until timestamp NULL,
	fk_dishes bigint NOT NULL,
	fk_menus bigint NOT NULL,
	PRIMARY KEY(fk_dishes, fk_menus),
	CONSTRAINT fkc_dishes FOREIGN KEY(fk_dishes) REFERENCES dishes (id),
	CONSTRAINT fkc_menus FOREIGN KEY(fk_menus) REFERENCES menus (id)
);

CREATE TABLE orders
(
	id bigserial NOT NULL primary key,
	date_time timestamp NULL DEFAULT CURRENT_DATE,
	temperature double precision NULL,
	submitted boolean NOT NULL,
	served boolean NOT NULL,
	fk_bills bigint NULL,
	fk_registered_users bigint NULL,
	fk_customer_tables bigint NOT NULL,
	CONSTRAINT fkc_bills FOREIGN KEY(fk_bills) REFERENCES bills (id),
	CONSTRAINT fkc_registered_users FOREIGN KEY(fk_registered_users) REFERENCES registered_users (id),
	CONSTRAINT fkc_customer_tables FOREIGN KEY(fk_customer_tables) REFERENCES customer_tables (id)
);

CREATE TABLE events
(
	id bigserial NOT NULL primary key,
	type bigint NOT NULL,
	fk_orders bigint NOT NULL,
	FOREIGN KEY(type) REFERENCES event_type (id),
	CONSTRAINT fkc_orders FOREIGN KEY(fk_orders) REFERENCES orders (id)
);

CREATE TABLE order_dishes
(
	quantity integer NOT NULL,
	comment varchar (255) NULL,
	fk_orders bigint NOT NULL,
	fk_dishes bigint NOT NULL,
	PRIMARY KEY(fk_dishes, fk_orders),
	CONSTRAINT fkc_orders FOREIGN KEY(fk_orders) REFERENCES orders (id),
	CONSTRAINT fkc_dishes FOREIGN KEY(fk_dishes) REFERENCES dishes (id)
);
