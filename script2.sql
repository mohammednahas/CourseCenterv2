START TRANSACTION;

UPDATE "Students"
SET
    "FirstName" = split_part(trim("Name"), ' ', 1),
    "LastName" =
        CASE
            WHEN position(' ' in trim("Name")) = 0 THEN ''
            ELSE ltrim(
                substring(
                    trim("Name")
                    FROM position(' ' in trim("Name")) + 1
                )
            )
        END;

ALTER TABLE "Students"
DROP COLUMN "Name";

COMMIT;