use TechServicePracticeDB;
CREATE TABLE Users
(
Id BIGINT Identity(1,1) PRIMARY KEY,
FIO VARCHAR(300) NOT NULL,
DateOfBirth DATE NOT NULL,
PhoneNumber VARCHAR(12) NOT NULL,
UserRoler VARCHAR(100) NOT NULL DEFAULT 'Client',
UserPassword VARCHAR(50) NOT NULL
);

CREATE TABLE Appeal
(
Id BIGINT Identity(1,1) PRIMARY KEY,
DateOfAppeal DATE NOT NULL,
AppealStatus VARCHAR(30) NOT NULL,
GadgetType VARCHAR(100) NOT NULL,
ProblemDescription VARCHAR(500) NOT NULL,
ClientId BIGINT NOT NULL
);

CREATE TABLE Request
(
Id BIGINT Identity(1,1) PRIMARY KEY,
ProblemType VARCHAR(300) NULL,
SerialNumber BIGINT NULL,
RequestPriority INT NOT NULL,
AppealId BIGINT NOT NULL,
ExecutorId BIGINT NULL,
CompleatingDate DATE NULL,
Commentary VARCHAR(500) NULL
);
ALTER TABLE Appeal ADD FOREIGN KEY(ClientId) REFERENCES Users(Id);
ALTER TABLE Request ADD FOREIGN KEY(ExecutorId) REFERENCES Users(Id);
ALTER TABLE Request ADD FOREIGN KEY(AppealId) REFERENCES Appeal(Id);