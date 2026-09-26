START TRANSACTION;

ALTER TABLE "Students"
ADD "FirstName" character varying(100) NOT NULL DEFAULT '';

ALTER TABLE "Students"
ADD "LastName" character varying(100) NOT NULL DEFAULT '';

ALTER TABLE "Students"
ADD "Mobile" character varying(20);

COMMIT;