--@(#) script.ddl

CREATE TABLE customer_tables
(
	id integer NOT NULL,
	seats_count integer NOT NULL,
	qr_code varchar (255) NULL,
	is_taken boolean NOT NULL,
	join_code varchar (255) NULL,
	PRIMARY KEY(id)
);

CREATE TABLE discounts
(
	id integer NOT NULL,
	discount_code varchar (255) NOT NULL,
	stand_until date NULL,
	discount_proc integer NOT NULL,
	PRIMARY KEY(id)
);

CREATE TABLE dish_categories
(
	id integer NOT NULL,
	title varchar (255) NOT NULL,
	PRIMARY KEY(id)
);

CREATE TABLE ingredients
(
	id integer NOT NULL,
	title varchar (255) NOT NULL,
	PRIMARY KEY(id)
);

CREATE TABLE menus
(
	id integer NOT NULL,
	title varchar (255) NOT NULL,
	mon boolean NOT NULL,
	tue boolean NOT NULL,
	wed boolean NOT NULL,
	thu boolean NOT NULL,
	fri boolean NOT NULL,
	sat boolean NOT NULL,
	sun boolean NOT NULL,
	time_from date NULL,
	time_until date NULL,
	date_from date NULL,
	date_until date NULL,
	is_active boolean NOT NULL,
	PRIMARY KEY(id)
);

CREATE TABLE event_type
(
	id integer NOT NULL,
	name char (12) NOT NULL,
	PRIMARY KEY(id)
);
INSERT INTO event_type(id, name) VALUES(1, 'bill_request');
INSERT INTO event_type(id, name) VALUES(2, 'cancel_order');
INSERT INTO event_type(id, name) VALUES(3, 'new_customer');
INSERT INTO event_type(id, name) VALUES(4, 'new_order');

CREATE TABLE user_role
(
	id integer NOT NULL,
	name char (13) NOT NULL,
	PRIMARY KEY(id)
);
INSERT INTO user_role(id, name) VALUES(1, 'administrator');
INSERT INTO user_role(id, name) VALUES(2, 'waiter');

CREATE TABLE bills
(
	id integer NOT NULL,
	date_time date NULL,
	tips double precision NULL,
	sum double precision NOT NULL,
	is_paid boolean NOT NULL,
	evaluation varchar (255) NOT NULL,
	fk_discounts integer NULL,
	PRIMARY KEY(id),
	CONSTRAINT fkc_discounts FOREIGN KEY(fk_discounts) REFERENCES discounts (id)
);

CREATE TABLE dishes
(
	id integer NOT NULL,
	title varchar (255) NOT NULL,
	description varchar (255) NULL,
	price double precision NOT NULL,
	calories integer NULL,
	discount double precision NULL,
	fk_dish_categories integer NULL,
	PRIMARY KEY(id),
	CONSTRAINT fkc_dish_categories FOREIGN KEY(fk_dish_categories) REFERENCES dish_categories (id)
);

CREATE TABLE registered_users
(
	id integer NOT NULL,
	name varchar (255) NOT NULL,
	surname varchar (255) NOT NULL,
	password varchar (255) NOT NULL,
	phone varchar (255) NULL,
	email varchar (255) NULL,
	birth_date date NOT NULL,
	is_blocked boolean NOT NULL,
	role integer NOT NULL,
	PRIMARY KEY(id),
	FOREIGN KEY(role) REFERENCES user_role (id)
);

CREATE TABLE dish_ingredients
(
	quantity double precision NOT NULL,
	fk_dishes integer NOT NULL,
	fk_ingredients integer NOT NULL,
	PRIMARY KEY(fk_dishes, fk_ingredients),
	CONSTRAINT fkc_dishes FOREIGN KEY(fk_dishes) REFERENCES dishes (id),
	CONSTRAINT fkc_ingredients FOREIGN KEY(fk_ingredients) REFERENCES ingredients (id)
);

CREATE TABLE menu_dishes
(
	date_from date NULL,
	date_until date NULL,
	fk_dishes integer NOT NULL,
	fk_menus integer NOT NULL,
	PRIMARY KEY(fk_dishes, fk_menus),
	CONSTRAINT fkc_dishes FOREIGN KEY(fk_dishes) REFERENCES dishes (id),
	CONSTRAINT fkc_menus FOREIGN KEY(fk_menus) REFERENCES menus (id)
);

CREATE TABLE orders
(
	id integer NOT NULL,
	date_time date NULL,
	temperature double precision NULL,
	submitted boolean NOT NULL,
	served boolean NOT NULL,
	fk_bills integer NULL,
	fk_registered_users integer NULL,
	fk_customer_tables integer NOT NULL,
	PRIMARY KEY(id),
	CONSTRAINT fkc_bills FOREIGN KEY(fk_bills) REFERENCES bills (id),
	CONSTRAINT fkc_registered_users FOREIGN KEY(fk_registered_users) REFERENCES registered_users (id),
	CONSTRAINT fkc_customer_tables FOREIGN KEY(fk_customer_tables) REFERENCES customer_tables (id)
);

CREATE TABLE events
(
	id integer NOT NULL,
	type integer NOT NULL,
	fk_orders integer NOT NULL,
	PRIMARY KEY(id),
	FOREIGN KEY(type) REFERENCES event_type (id),
	CONSTRAINT fkc_orders FOREIGN KEY(fk_orders) REFERENCES orders (id)
);

CREATE TABLE order_dishes
(
	quantity integer NOT NULL,
	comment varchar (255) NULL,
	fk_orders integer NOT NULL,
	fk_dishes integer NOT NULL,
	PRIMARY KEY(fk_dishes, fk_orders),
	CONSTRAINT fkc_orders FOREIGN KEY(fk_orders) REFERENCES orders (id),
	CONSTRAINT fkc_dishes FOREIGN KEY(fk_dishes) REFERENCES dishes (id)
);
