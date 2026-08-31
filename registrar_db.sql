-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Aug 31, 2026 at 07:15 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `registrar_db`
--

-- --------------------------------------------------------

--
-- Table structure for table `tbldocuments`
--

CREATE TABLE `tbldocuments` (
  `DocumentID` int(11) NOT NULL,
  `DocumentName` varchar(100) NOT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `Fee` decimal(10,2) NOT NULL DEFAULT 0.00,
  `Status` enum('Active','Inactive') DEFAULT 'Active'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbldocuments`
--

INSERT INTO `tbldocuments` (`DocumentID`, `DocumentName`, `Description`, `Fee`, `Status`) VALUES
(1, 'Transcript of Records', 'Official TOR', 150.00, 'Active'),
(2, 'Certificate of Enrollment', 'Proof of enrollment', 50.00, 'Active'),
(3, 'Certificate of Good Moral', 'Character certificate', 100.00, 'Active'),
(4, 'Certification', 'General certification', 50.00, 'Active'),
(5, 'Honorable Dismissal', 'Transfer clearance', 100.00, 'Active');

-- --------------------------------------------------------

--
-- Table structure for table `tblrequest`
--

CREATE TABLE `tblrequest` (
  `RequestID` int(11) NOT NULL,
  `RequestNo` varchar(20) NOT NULL,
  `StudentID` varchar(20) NOT NULL,
  `RequestDate` date NOT NULL,
  `TotalAmount` decimal(10,2) DEFAULT 0.00,
  `PaymentStatus` enum('Unpaid','Paid') DEFAULT 'Unpaid',
  `ORNo` varchar(30) DEFAULT NULL,
  `ORDate` date DEFAULT NULL,
  `Status` enum('Pending','Processing','Ready for Release','Released','Cancelled') DEFAULT 'Pending',
  `CreatedBy` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tblrequest`
--

INSERT INTO `tblrequest` (`RequestID`, `RequestNo`, `StudentID`, `RequestDate`, `TotalAmount`, `PaymentStatus`, `ORNo`, `ORDate`, `Status`, `CreatedBy`) VALUES
(1, 'REQ-2026-001', '1123-24', '2026-02-01', 150.00, 'Paid', 'OR-10001', '2026-02-01', '', 1),
(2, 'REQ-2026-002', '1127-24', '2026-02-01', 50.00, 'Paid', 'OR-10002', '2026-02-01', '', 2),
(3, 'REQ-2026-003', '1208-24', '2026-02-02', 100.00, '', NULL, NULL, 'Pending', 1),
(4, 'REQ-2026-004', '1314-24', '2026-02-02', 200.00, 'Paid', 'OR-10003', '2026-02-02', 'Processing', 2),
(5, 'REQ-2026-005', '1327-24', '2026-02-03', 50.00, 'Paid', 'OR-10004', '2026-02-03', '', 1),
(6, 'REQ-2026-006', '1395-24', '2026-02-03', 100.00, '', NULL, NULL, 'Pending', 2),
(7, 'REQ-2026-007', '1396-24', '2026-02-04', 150.00, 'Paid', 'OR-10005', '2026-02-04', 'Processing', 1),
(8, 'REQ-2026-008', '1522-24', '2026-02-04', 50.00, 'Paid', 'OR-10006', '2026-02-04', '', 2),
(9, 'REQ-2026-009', '1808-23', '2026-02-05', 250.00, 'Paid', 'OR-10007', '2026-02-05', 'Processing', 1),
(10, 'REQ-2026-010', '2055-24', '2026-02-05', 100.00, '', NULL, NULL, 'Pending', 2),
(11, 'REQ-2026-011', '2096-24', '2026-02-06', 150.00, 'Paid', 'OR-10008', '2026-02-06', '', 1),
(12, 'REQ-2026-012', '2194-24', '2026-02-06', 50.00, '', NULL, NULL, 'Cancelled', 2),
(13, 'REQ-2026-013', '2208-24', '2026-02-07', 100.00, 'Paid', 'OR-10009', '2026-02-07', 'Processing', 1),
(14, 'REQ-2026-014', '2786-24', '2026-02-07', 200.00, 'Paid', 'OR-10010', '2026-02-07', '', 2),
(15, 'REQ-2026-015', '2789-24', '2026-02-08', 50.00, '', NULL, NULL, 'Pending', 1);

-- --------------------------------------------------------

--
-- Table structure for table `tblrequestdetails`
--

CREATE TABLE `tblrequestdetails` (
  `RequestDetailID` int(11) NOT NULL,
  `RequestID` int(11) NOT NULL,
  `DocumentID` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL DEFAULT 1,
  `Amount` decimal(10,2) NOT NULL,
  `SubTotal` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tblstudents`
--

CREATE TABLE `tblstudents` (
  `StudentID` varchar(20) NOT NULL,
  `LRN` varchar(20) DEFAULT NULL,
  `LastName` varchar(50) NOT NULL,
  `FirstName` varchar(50) NOT NULL,
  `MiddleName` varchar(50) DEFAULT NULL,
  `Course` varchar(50) DEFAULT NULL,
  `YearLevel` varchar(20) DEFAULT NULL,
  `Section` varchar(20) DEFAULT NULL,
  `ContactNo` varchar(20) DEFAULT NULL,
  `Status` enum('Active','Inactive') DEFAULT 'Active'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tblstudents`
--

INSERT INTO `tblstudents` (`StudentID`, `LRN`, `LastName`, `FirstName`, `MiddleName`, `Course`, `YearLevel`, `Section`, `ContactNo`, `Status`) VALUES
('1123-24', '098760987612', 'Fernandez', 'Gio', 'Natividad', 'BSIT', '3rd Year', '31E1', '09609829478', 'Active'),
('1127-24', '123456789016', 'Enclona', 'Paul Benedict', NULL, 'BSIT', '3rd Year', '31E1', '0912345678904', 'Active'),
('1208-24', '123456789018', 'Para', 'Andrea', NULL, 'BSIT', '3rd Year', '31E1', '0912345678906', 'Active'),
('1314-24', '253435623456', 'Batoy', 'Nicholo John', 'Lopez', 'BSIT', '3rd Year', '31E1', '09676781233', 'Active'),
('1327-24', '123456789015', 'Reales', 'Jonnidel', 'Paradero', 'BSIT', '3rd Year', '31E1', '0912345678903', 'Active'),
('1395-24', '407321150214', 'Solis', 'Sophia Cassandra', 'Villacorte', 'BSIT', '3rd Year', '31E1', '09690141523', 'Active'),
('1396-24', '123456123412', 'Mendoza', 'Stephanie', 'Pobar', 'BSIT', '3rd Year', '31E1', '09612564233', 'Active'),
('1522-24', '234567345678', 'Barcinas', 'Marc Denize', 'Babon', 'BSIT', '3rd Year', '31E1', '09612564765', 'Active'),
('1808-23', '123456789013', 'Villacorte', 'Joshua', 'Joseph', 'BSIT', '3rd Year', '31E1', '0912345678901', 'Active'),
('2055-24', '123456789012', 'Canua', 'Carl James', 'Prado', 'BSIT', '3rd Year', '31E3', '09764743381', 'Active'),
('2096-24', '123456789019', 'Ramones', 'Leisbeth', 'Bering', 'BSIT', '3rd Year', '31E1', '0912345678907', 'Active'),
('2194-24', '123456789014', 'Sabesaje', 'Sho Uno', NULL, 'BSIT', '3rd Year', '31E1', '0912345678902', 'Active'),
('2208-24', '123456789017', 'Eullo', 'John Raven', 'Jandoc', 'BSIT', '3rd Year', '31E1', '0912345678905', 'Active'),
('2786-24', '424515150094', 'Roque', 'Kevin Clerck', 'Alora', 'BSIT', '3rd Year', '31E1', '09626728466', 'Active'),
('2789-24', '136899110095', 'De Vera', 'Alliyah', 'Garcia', 'BSIT', '3rd Year', '31E1', '09625632435', 'Active');

-- --------------------------------------------------------

--
-- Table structure for table `tblusers`
--

CREATE TABLE `tblusers` (
  `UserID` int(11) NOT NULL,
  `Username` varchar(50) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `FullName` varchar(100) NOT NULL,
  `Role` enum('Administrator','Registrar Staff') NOT NULL,
  `Status` enum('Active','Inactive') DEFAULT 'Active'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tblusers`
--

INSERT INTO `tblusers` (`UserID`, `Username`, `Password`, `FullName`, `Role`, `Status`) VALUES
(1, 'admin', 'admin123', 'System Administrator', 'Administrator', 'Active'),
(2, 'staff1', 'staff123', 'Registrar Staff One', 'Registrar Staff', 'Active'),
(3, 'staff2', 'staff456', 'Registrar Staff Two', 'Registrar Staff', 'Active'),
(4, 'encoder1', 'encoder123', 'Data Encoder One', 'Registrar Staff', 'Active');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `tbldocuments`
--
ALTER TABLE `tbldocuments`
  ADD PRIMARY KEY (`DocumentID`);

--
-- Indexes for table `tblrequest`
--
ALTER TABLE `tblrequest`
  ADD PRIMARY KEY (`RequestID`),
  ADD UNIQUE KEY `RequestNo` (`RequestNo`),
  ADD KEY `StudentID` (`StudentID`),
  ADD KEY `CreatedBy` (`CreatedBy`);

--
-- Indexes for table `tblrequestdetails`
--
ALTER TABLE `tblrequestdetails`
  ADD PRIMARY KEY (`RequestDetailID`),
  ADD KEY `RequestID` (`RequestID`),
  ADD KEY `DocumentID` (`DocumentID`);

--
-- Indexes for table `tblstudents`
--
ALTER TABLE `tblstudents`
  ADD PRIMARY KEY (`StudentID`);

--
-- Indexes for table `tblusers`
--
ALTER TABLE `tblusers`
  ADD PRIMARY KEY (`UserID`),
  ADD UNIQUE KEY `Username` (`Username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `tbldocuments`
--
ALTER TABLE `tbldocuments`
  MODIFY `DocumentID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `tblrequest`
--
ALTER TABLE `tblrequest`
  MODIFY `RequestID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT for table `tblrequestdetails`
--
ALTER TABLE `tblrequestdetails`
  MODIFY `RequestDetailID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tblusers`
--
ALTER TABLE `tblusers`
  MODIFY `UserID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `tblrequest`
--
ALTER TABLE `tblrequest`
  ADD CONSTRAINT `tblrequest_ibfk_1` FOREIGN KEY (`StudentID`) REFERENCES `tblstudents` (`StudentID`),
  ADD CONSTRAINT `tblrequest_ibfk_2` FOREIGN KEY (`CreatedBy`) REFERENCES `tblusers` (`UserID`);

--
-- Constraints for table `tblrequestdetails`
--
ALTER TABLE `tblrequestdetails`
  ADD CONSTRAINT `tblrequestdetails_ibfk_1` FOREIGN KEY (`RequestID`) REFERENCES `tblrequest` (`RequestID`),
  ADD CONSTRAINT `tblrequestdetails_ibfk_2` FOREIGN KEY (`DocumentID`) REFERENCES `tbldocuments` (`DocumentID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
