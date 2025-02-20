USE Hotels

-- Table Roles
BEGIN SET IDENTITY_INSERT Roles ON;
    INSERT INTO Roles (Id, Name) VALUES (1,'Agency');
END SET IDENTITY_INSERT Roles OFF;

-- Table Users
BEGIN
    INSERT INTO Users (Username, Email, Password, RoleId)
    VALUES ('Agency User', 'agency@example.com', '$2a$12$eX2TlnPL0bD3j7.MqzHvvuV0w99B5EZb3Pj9Gt.yp21N4u6XkgS8.', 1);
END

SELECT * FROM Roles
