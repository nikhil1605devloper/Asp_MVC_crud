use CrudDemoDb;

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY,
    Username VARCHAR(100),
    Password VARCHAR(100)
)

INSERT INTO Users (Username, Password) VALUES ('admin', '1234')
