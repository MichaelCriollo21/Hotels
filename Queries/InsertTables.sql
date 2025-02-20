USE Hotels;

-- Insert Roles
SET IDENTITY_INSERT Roles ON;
INSERT INTO Roles (Id, Name) VALUES (1, 'Agency');
SET IDENTITY_INSERT Roles OFF;

-- Insert Users
SET IDENTITY_INSERT Users ON;
INSERT INTO Users (Id, RoleId, FirstName, LastName, Email, PasswordHash, PhoneNumber)
VALUES 
    (1, 1, 'Juan', 'Perez', 'juan.perez@example.com', '$2a$12$/uH.R6atCti5xO1bGweJa.MEewl40aelDRQ2nqHLRtNMj7o3dgRVy', '3001234567'), -- La contraseña es "Password123!"
    (2, 1, 'Anna', 'Smith', 'anna.smith@example.com', '$2a$12$/uH.R6atCti5xO1bGweJa.MEewl40aelDRQ2nqHLRtNMj7o3dgRVy', '3109876543');
SET IDENTITY_INSERT Users OFF;

-- Insert Hotels
SET IDENTITY_INSERT Hotels ON;
INSERT INTO Hotels (Id, Name, Address, City, Country, IsEnabled, AgentId)
VALUES 
    (1, 'Hotel Plaza', '123 Street #45-67', 'Bogotá', 'Colombia', 1, 1),
    (2, 'Hotel Riviera', 'Main Avenue 89', 'Cartagena', 'Colombia', 1, 1);
SET IDENTITY_INSERT Hotels OFF;

-- Insert Rooms
SET IDENTITY_INSERT Rooms ON;
INSERT INTO Rooms (Id, HotelId, Type, BaseCost, Taxes, Location, IsEnabled, MaxCapacity)
VALUES 
    (1, 1, 'Suite', 200.00, 30.00, '5th Floor, Room 501', 1, 4),
    (2, 2, 'Double', 150.00, 20.00, '2nd Floor, Room 203', 1, 2);
SET IDENTITY_INSERT Rooms OFF;

-- Insert Bookings
SET IDENTITY_INSERT Bookings ON;
INSERT INTO Bookings (Id, UserId, RoomId, CheckInDate, CheckOutDate, NumberOfGuests, TotalCost, IsConfirmed)
VALUES 
    (1, 2, 1, '2025-03-10', '2025-03-15', 2, 460.00, 1),
    (2, 2, 2, '2025-04-01', '2025-04-05', 1, 170.00, 0);
SET IDENTITY_INSERT Bookings OFF;

-- Insert Passengers
SET IDENTITY_INSERT Passengers ON;
INSERT INTO Passengers (Id, BookingId, FirstName, LastName, DateOfBirth, Gender, DocumentType, DocumentNumber, Email, PhoneNumber)
VALUES 
    (1, 1, 'Carlos', 'Rodriguez', '1990-05-21', 'Male', 'ID Card', '123456789', 'carlos.rod@example.com', '3216549870'),
    (2, 2, 'Maria', 'Lopez', '1985-08-30', 'Female', 'Passport', 'A1234567', 'maria.lopez@example.com', '3004567891');
SET IDENTITY_INSERT Passengers OFF;

-- Insert Emergency Contacts
SET IDENTITY_INSERT EmergencyContacts ON;
INSERT INTO EmergencyContacts (Id, BookingId, FullName, ContactPhone)
VALUES 
    (1, 1, 'Luis Perez', '3201112233'),
    (2, 2, 'Sofia Gomez', '3159876543');
SET IDENTITY_INSERT EmergencyContacts OFF;
