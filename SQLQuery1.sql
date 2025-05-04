CREATE TABLE Admin (
    AdminId INT IDENTITY(1,1) PRIMARY KEY,  -- Auto-incrementing primary key
    FirstName NVARCHAR(50) NOT NULL,        -- Admin's first name
    LastName NVARCHAR(50) NOT NULL,         -- Admin's last name
    Email NVARCHAR(100) NOT NULL UNIQUE,    -- Unique email for login
    PasswordHash NVARCHAR(255) NOT NULL,    -- Password hash for security
    PhoneNumber NVARCHAR(15),               -- Optional contact number
    CreatedAt DATETIME DEFAULT GETDATE(),   -- Record creation date
    IsActive BIT DEFAULT 1                  -- Active status (1 = Active, 0 = Inactive)
);


INSERT INTO Admin (FirstName, LastName, Email, PasswordHash, PhoneNumber)
VALUES ('John', 'Doe', 'admin@example.com', 'admin123', '0771234567');



-- Create Citizens Table
CREATE TABLE Citizens (
    CitizenID INT IDENTITY(1,1) PRIMARY KEY, -- Auto-increment in MS SQL
    FullName NVARCHAR(100) NOT NULL, -- Use NVARCHAR for Unicode support
    Email NVARCHAR(100) NOT NULL UNIQUE, -- UNIQUE constraint for Email
    PasswordHash NVARCHAR(255) NOT NULL, -- Use NVARCHAR for PasswordHash
    PhoneNumber NVARCHAR(15) NOT NULL UNIQUE, -- UNIQUE constraint for PhoneNumber
    Address NVARCHAR(MAX), -- Use NVARCHAR(MAX) for variable-length text
    DateOfBirth DATE,
    NationalID NVARCHAR(50) UNIQUE, -- UNIQUE constraint for NationalID
    CreatedAt DATETIME DEFAULT GETDATE(), -- Use GETDATE() for default timestamp
    UpdatedAt DATETIME DEFAULT GETDATE() -- UpdatedAt column
);

-- Create trigger to update the UpdatedAt column on row update
CREATE TRIGGER trg_UpdateCitizensUpdatedAt
ON Citizens
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Citizens
    SET UpdatedAt = GETDATE()
    WHERE CitizenID IN (SELECT DISTINCT CitizenID FROM Inserted);
END;

Drop TRIGGER trg_UpdateCitizensUpdatedAt;

-- Insert sample record 1
INSERT INTO Citizens (FullName, Email, PasswordHash, PhoneNumber, Address, DateOfBirth, NationalID)
VALUES 
('John Doe', 'johndoe@example.com', 'hashedpassword123', '+1234567890', '123 Elm Street, Springfield', '1985-06-15', 'ABC1234567');

-- Insert sample record 2
INSERT INTO Citizens (FullName, Email, PasswordHash, PhoneNumber, Address, DateOfBirth, NationalID)
VALUES 
('Jane Smith', 'janesmith@example.com', 'hashedpassword456', '+1987654321', '456 Oak Avenue, Shelbyville', '1990-09-22', 'XYZ9876543');



CREATE TABLE Complaints (
    ComplaintID INT PRIMARY KEY IDENTITY(1,1),
    CitizenID INT NOT NULL,
    ComplaintTitle NVARCHAR(100) NOT NULL,
    ComplaintDescription NVARCHAR(MAX) NOT NULL,
    ComplaintStatus NVARCHAR(50) DEFAULT 'Pending',
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY (CitizenID) REFERENCES Citizens(CitizenID)
);

CREATE TABLE PoliceRoles (
    RoleID INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the role
    RoleName NVARCHAR(50) NOT NULL UNIQUE -- Name of the role (e.g., Administrator, Police Officer)
);

CREATE TABLE PoliceStations (
    StationID INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the station
    StationName NVARCHAR(100) NOT NULL, -- Name of the station
    Location NVARCHAR(MAX) NOT NULL -- Address or location details
);

CREATE TABLE PoliceOfficers (
    OfficerID INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the officer
    FullName NVARCHAR(100) NOT NULL, -- Officer's full name
    Email NVARCHAR(100) NOT NULL UNIQUE, -- Email for login/communication
    PhoneNumber NVARCHAR(15) NULL, -- Contact number
    RoleID INT NOT NULL, -- Foreign key linking to PoliceRoles table
    StationID INT NOT NULL, -- ID of the station the officer is assigned to
    DateJoined DATETIME DEFAULT GETDATE(), -- Date when the officer joined
    IsActive BIT DEFAULT 1, -- Status of the officer (active/inactive)
    PasswordHash NVARCHAR(MAX) NOT NULL, -- Password hash for authentication
    FOREIGN KEY (RoleID) REFERENCES PoliceRoles(RoleID) ON DELETE CASCADE, -- Cascade deletion on role
    FOREIGN KEY (StationID) REFERENCES PoliceStations(StationID) ON DELETE CASCADE -- Cascade deletion on station
);

CREATE TABLE CybercrimeReports (
    ReportID INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for each report
    CitizenID INT NOT NULL, -- Foreign key linking the report to a citizen
    ReportType NVARCHAR(100) NOT NULL, -- Type of cybercrime (e.g., phishing, hacking)
    Description NVARCHAR(MAX) NOT NULL, -- Detailed description of the incident
    EvidenceURL NVARCHAR(MAX) NULL, -- URL or path for uploaded evidence (screenshots, files)
    ReportStatus NVARCHAR(50) DEFAULT 'Pending', -- Status of the report (e.g., Pending, Investigating, Resolved)
    DateReported DATETIME DEFAULT GETDATE(), -- Date and time the report was submitted
    LastUpdated DATETIME DEFAULT GETDATE(), -- Date and time the report was last updated
    AssignedOfficerID INT NULL, -- Foreign key linking to the officer handling the report
    FOREIGN KEY (CitizenID) REFERENCES Citizens(CitizenID), -- Linking to the Citizens table
    FOREIGN KEY (AssignedOfficerID) REFERENCES PoliceOfficers(OfficerID) -- Linking to the Police Officers table
);


INSERT INTO CybercrimeReports (CitizenID, ReportType, Description, EvidenceURL, ReportStatus)
VALUES
(8, 'Phishing', 'A phishing attack was carried out via email, tricking the user into revealing sensitive account information. The email was designed to appear as a legitimate bank communication.', 'http://example.com/screenshot1.png', 'Pending'),

(9, 'Ransomware', 'The victimÅfs computer was infected with ransomware, which encrypted personal files and demanded a ransom for decryption keys. The attack was detected on January 22, 2025.', 'http://example.com/screenshot2.png', 'Pending');



CREATE TABLE Feedback (
    FeedbackID INT PRIMARY KEY IDENTITY(1,1),
    FeedbackType NVARCHAR(50) NOT NULL,
    Rating INT CHECK (Rating BETWEEN 0 AND 5) NOT NULL,
    Comments NVARCHAR(MAX) NOT NULL,
    SubmittedAt DATETIME DEFAULT GETDATE()
);
EXEC sp_rename 'Feedback', 'Feedbacks';



CREATE TABLE SecurityRequests (
    RequestID INT IDENTITY(1,1) PRIMARY KEY,       -- Auto-incrementing unique ID for each request
    CitizenID INT NOT NULL,                       -- Foreign key to identify the requesting citizen
    RequestReason NVARCHAR(MAX),                  -- Reason for requesting security
    Location NVARCHAR(255),                       -- Location where security is needed
    DateRequested DATETIME DEFAULT GETDATE(),     -- Timestamp of when the request was created
    SecurityDate DATE NOT NULL,                   -- The specific date when security is needed
    Description NVARCHAR(MAX),                    -- Additional description about the request
    Status NVARCHAR(50) DEFAULT 'Pending',        -- Status of the request (e.g., Pending, Approved, Rejected)
    AssignedOfficerID INT NULL,                   -- Officer assigned to fulfill the request (optional)
    FOREIGN KEY (CitizenID) REFERENCES Citizens(CitizenID) ON DELETE CASCADE, -- Link to Citizens table
    FOREIGN KEY (AssignedOfficerID) REFERENCES PoliceOfficers(OfficerID) ON DELETE SET NULL -- Optional officer assignment
);


-- Insert sample records for CitizenID 8
INSERT INTO SecurityRequests (CitizenID, RequestReason, Location, SecurityDate, Description, Status, AssignedOfficerID)
VALUES 
(8, 'Security required for a wedding event', 'Community Hall', '2025-02-15', 'Large gathering expected, requiring crowd control.', 'Pending', NULL);


INSERT INTO SecurityRequests (CitizenID, RequestReason, Location, SecurityDate, Description, Status, AssignedOfficerID)
VALUES 
(9, 'Security for public meeting', 'Central Park', '2025-02-28', 'Expected crowd of over 500 people for the meeting.', 'Pending', NULL);



CREATE TABLE AccidentReports (
    ReportID INT PRIMARY KEY IDENTITY(1,1),
    CitizenID INT NOT NULL,
    AccidentType NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Location NVARCHAR(255) NOT NULL,
    DateReported DATETIME DEFAULT GETDATE(),
    IsResolved BIT DEFAULT 0,
    ResponderID INT NULL,
    FOREIGN KEY (CitizenID) REFERENCES Citizens(CitizenID) ON DELETE CASCADE,
    FOREIGN KEY (ResponderID) REFERENCES PoliceOfficers(OfficerID) ON DELETE SET NULL
);


INSERT INTO AccidentReports (CitizenID, AccidentType, Description, Location)  
VALUES  
(8, 'Vehicle Collision', 'Minor accident involving two cars at an intersection.', 'Main Street, Colombo'),  
(9, 'Hit and Run', 'Pedestrian hit by a speeding motorcycle, no details on the rider.', 'Galle Road, Galle'),  
(8, 'Property Damage', 'A truck crashed into a shop, causing structural damage.', 'Kandy City Center, Kandy'),  
(9, 'Slip and Fall', 'A person slipped on an oil spill near a gas station.', 'Petrol Station, Negombo');  


CREATE TABLE RobberyReports (
    ReportID INT PRIMARY KEY IDENTITY(1,1),
    CitizenID INT NOT NULL,
    RobberyType NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Location NVARCHAR(255) NOT NULL,
    RobberyDate DATETIME NOT NULL, -- New: Date when the robbery happened
    StolenItems NVARCHAR(MAX) NULL, -- New: List of stolen/misplaced items
    DateReported DATETIME DEFAULT GETDATE(),
    IsResolved BIT DEFAULT 0,
    ResponderID INT NULL,
    FOREIGN KEY (CitizenID) REFERENCES Citizens(CitizenID) ON DELETE CASCADE,
    FOREIGN KEY (ResponderID) REFERENCES PoliceOfficers(OfficerID) ON DELETE SET NULL
);


INSERT INTO RobberyReports (CitizenID, RobberyType, Description, Location, RobberyDate, StolenItems)
VALUES  
(8, 'Pickpocketing', 'Wallet stolen in a crowded market.', 'Main Street, Colombo', '2025-01-20 14:30:00', 'Wallet, ID card, Credit Card'),  
(9, 'Burglary', 'House broken into at night.', 'Galle Road, Galle', '2025-01-18 02:00:00', 'Laptop, Gold necklace, Cash'),  
(8, 'Mugging', 'Robbed at knifepoint near a bus stop.', 'Kandy City Center, Kandy', '2025-01-25 19:15:00', 'Mobile phone, Watch, Backpack'),  
(9, 'Snatch Theft', 'Motorbike snatchers stole a handbag.', 'Negombo Beach Road, Negombo', '2025-01-22 17:45:00', 'Handbag, Passport, Sunglasses');  


CREATE TABLE TrafficReports (
    ReportID INT PRIMARY KEY IDENTITY(1,1),
    CitizenID INT NOT NULL,
    TrafficType NVARCHAR(100) NOT NULL, -- Type of traffic issue (e.g., Accident, Heavy Congestion)
    Location NVARCHAR(255) NOT NULL, -- Where the traffic issue occurred
    SeverityLevel INT NOT NULL CHECK (SeverityLevel BETWEEN 1 AND 5), -- 1 (Low) to 5 (Severe)
    DateReported DATETIME DEFAULT GETDATE(), -- When it was reported
    IsResolved BIT DEFAULT 0, -- Status (Resolved or Not)
    ResponderID INT NULL, -- Police Officer handling the case
    FOREIGN KEY (CitizenID) REFERENCES Citizens(CitizenID) ON DELETE CASCADE,
    FOREIGN KEY (ResponderID) REFERENCES PoliceOfficers(OfficerID) ON DELETE SET NULL
);

INSERT INTO TrafficReports (CitizenID, TrafficType, Location, SeverityLevel)
VALUES  
(8, 'Accident', 'Colombo-Katunayake Expressway', 4),  
(9, 'Heavy Traffic', 'Galle Road, Colombo', 3),  
(8, 'Roadblock', 'Negombo Road, Gampaha', 5),  
(9, 'Accident', 'Kandy Town Center', 2);  



CREATE TABLE PoliceReports (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CitizenID INT NOT NULL,
    ReportType NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    DateRequested DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_PoliceReports_Citizens FOREIGN KEY (CitizenID) REFERENCES Citizens(CitizenID) ON DELETE CASCADE
);

INSERT INTO PoliceReports (CitizenID, ReportType, Description, Status, DateRequested)
VALUES
(8, 'Police Clearance Certificate', 'Request for police clearance certificate for job application.', 'Pending', GETDATE()),
(8, 'Lost Property Report', 'Request for a report on lost passport for travel purposes.', 'Pending', GETDATE()),
(9, 'Incident Report', 'Request for incident report regarding a vehicle accident for insurance purposes.', 'Pending', GETDATE()),
(9, 'Theft Report', 'Request for a theft report to provide to the bank for insurance claim.', 'Pending', GETDATE());


CREATE TABLE Fines (
    FineID INT IDENTITY(1,1) PRIMARY KEY,  -- Unique identifier for each fine
    CitizenID INT NOT NULL,  -- References the citizen receiving the fine
    FineAmount DECIMAL(10,2) NOT NULL,  -- Amount of the fine
    FineReason NVARCHAR(255) NOT NULL,  -- Reason for the fine
    IssueDate DATETIME DEFAULT GETDATE(),  -- Date the fine was issued
    DueDate DATETIME,  -- Due date for payment
    Status NVARCHAR(50) DEFAULT 'Unpaid',  -- Fine status: 'Unpaid', 'Paid', 'Overdue'
    PaymentDate DATETIME NULL,  -- Date when the fine was paid (NULL if unpaid)
    TransactionID NVARCHAR(100) NULL,  -- Stores Stripe or other payment transaction ID
    CONSTRAINT FK_Citizen_Fines FOREIGN KEY (CitizenID) REFERENCES Citizens(CitizenID) ON DELETE CASCADE
);

INSERT INTO Fines (CitizenID, FineAmount, FineReason, IssueDate, DueDate, Status, PaymentDate, TransactionID)
VALUES 
(8, 5000.00, 'Speeding Violation', '2025-03-15', '2025-04-15', 'Unpaid', NULL, NULL),
(8, 7500.00, 'Illegal Parking', '2025-03-10', '2025-04-10', 'Paid', '2025-03-12', 'TXN123456'),

(9, 12000.00, 'Driving Without License', '2025-02-25', '2025-03-25', 'Unpaid', NULL, NULL),
(9, 3000.00, 'Helmet Violation', '2025-03-05', '2025-04-05', 'Paid', '2025-03-07', 'TXN654321');