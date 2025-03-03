USE Hotels

CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    RoleId INT NOT NULL,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(20),
    FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);

CREATE TABLE Hotels (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    City NVARCHAR(100) NOT NULL,
    Country NVARCHAR(100) NOT NULL,
	IsEnabled BIT NOT NULL DEFAULT 1,
    AgentId INT NOT NULL,
    FOREIGN KEY (AgentId) REFERENCES Users(Id)
);

CREATE TABLE Rooms (
    Id INT PRIMARY KEY IDENTITY(1,1),
    HotelId INT NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    BaseCost DECIMAL(10,2) NOT NULL,
    Taxes DECIMAL(10,2) NOT NULL,
    Location NVARCHAR(255) NOT NULL,
    IsEnabled BIT NOT NULL DEFAULT 1,
    MaxCapacity INT NOT NULL,
    FOREIGN KEY (HotelId) REFERENCES Hotels(Id)
);

CREATE TABLE Bookings (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    RoomId INT NOT NULL,
    CheckInDate DATE NOT NULL,
    CheckOutDate DATE NOT NULL,
    NumberOfGuests INT NOT NULL,
    TotalCost DECIMAL(10,2) NOT NULL,
    IsConfirmed BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (RoomId) REFERENCES Rooms(Id)
);

CREATE TABLE Passengers (
    Id INT PRIMARY KEY IDENTITY(1,1),
    BookingId INT NOT NULL,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender NVARCHAR(20) NOT NULL,
    DocumentType NVARCHAR(50) NOT NULL,
    DocumentNumber NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    FOREIGN KEY (BookingId) REFERENCES Bookings(Id)
);

CREATE TABLE EmergencyContacts (
    Id INT PRIMARY KEY IDENTITY(1,1),
    BookingId INT NOT NULL,
    FullName NVARCHAR(200) NOT NULL,
    ContactPhone NVARCHAR(20) NOT NULL,
    FOREIGN KEY (BookingId) REFERENCES Bookings(Id)
);
