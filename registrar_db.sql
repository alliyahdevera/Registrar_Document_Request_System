-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: Sep 24, 2026 at 08:14 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

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
  `AmountPaid` decimal(10,2) DEFAULT 0.00,
  `ORNo` varchar(30) DEFAULT NULL,
  `ORDate` date DEFAULT NULL,
  `Status` enum('Pending','Processing','Ready for Release','Released','Cancelled') DEFAULT 'Pending',
  `CreatedBy` int(11) DEFAULT NULL,
  `ProcessedBy` int(11) DEFAULT NULL,
  `ReleasedBy` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tblrequest`
--

INSERT INTO `tblrequest` (`RequestID`, `RequestNo`, `StudentID`, `RequestDate`, `TotalAmount`, `PaymentStatus`, `AmountPaid`, `ORNo`, `ORDate`, `Status`, `CreatedBy`, `ProcessedBy`, `ReleasedBy`) VALUES
(1, 'REQ-2026-001', '1123-24', '2026-02-01', 150.00, 'Paid', 150.00, 'OR-10001', '2026-02-01', 'Cancelled', 4, NULL, NULL),
(2, 'REQ-2026-002', '1127-24', '2026-02-01', 50.00, 'Paid', 50.00, 'OR-10002', '2026-02-01', 'Cancelled', 2, NULL, NULL),
(3, 'REQ-2026-003', '1208-24', '2026-02-02', 100.00, '', 0.00, NULL, NULL, 'Cancelled', 4, NULL, NULL),
(4, 'REQ-2026-004', '1314-24', '2026-02-02', 200.00, 'Paid', 200.00, 'OR-10003', '2026-02-02', 'Processing', 2, 2, NULL),
(5, 'REQ-2026-005', '1327-24', '2026-02-03', 50.00, 'Paid', 50.00, 'OR-10004', '2026-02-03', 'Cancelled', 4, NULL, NULL),
(6, 'REQ-2026-006', '1395-24', '2026-02-03', 100.00, '', 0.00, NULL, NULL, 'Cancelled', 2, NULL, NULL),
(7, 'REQ-2026-007', '1396-24', '2026-02-04', 150.00, 'Paid', 150.00, 'OR-10005', '2026-02-04', 'Processing', 4, 2, NULL),
(8, 'REQ-2026-008', '1522-24', '2026-02-04', 50.00, 'Paid', 50.00, 'OR-10006', '2026-02-04', 'Cancelled', 2, NULL, NULL),
(9, 'REQ-2026-009', '1808-23', '2026-02-05', 250.00, 'Paid', 250.00, 'OR-10007', '2026-02-05', 'Released', 4, 2, 2),
(10, 'REQ-2026-010', '2055-24', '2026-02-05', 100.00, '', 0.00, NULL, NULL, 'Cancelled', 2, NULL, NULL),
(11, 'REQ-2026-011', '2096-24', '2026-02-06', 150.00, 'Paid', 150.00, 'OR-10008', '2026-02-06', 'Cancelled', 4, NULL, NULL),
(12, 'REQ-2026-012', '2194-24', '2026-02-06', 50.00, '', 0.00, NULL, NULL, 'Cancelled', 2, NULL, NULL),
(13, 'REQ-2026-013', '2208-24', '2026-02-07', 100.00, 'Paid', 100.00, 'OR-10009', '2026-02-07', 'Released', 4, 4, 2),
(14, 'REQ-2026-014', '2786-24', '2026-02-07', 200.00, 'Paid', 200.00, 'OR-10010', '2026-02-07', 'Cancelled', 2, NULL, NULL),
(15, 'REQ-2026-015', '2789-24', '2026-02-08', 50.00, '', 0.00, NULL, NULL, 'Cancelled', 4, NULL, NULL),
(16, 'REQ-2026-016', '1395-24', '2026-09-22', 50.00, 'Unpaid', 0.00, NULL, NULL, 'Pending', 4, NULL, NULL),
(17, 'REQ-2024-001', '1123-24', '2024-03-15', 150.00, 'Paid', 150.00, NULL, NULL, 'Released', 4, 2, 3),
(18, 'REQ-2024-002', '1127-24', '2024-05-20', 50.00, 'Paid', 50.00, NULL, NULL, 'Released', 4, 2, 2),
(19, 'REQ-2024-003', '1208-24', '2024-08-10', 200.00, 'Paid', 200.00, NULL, NULL, 'Released', 2, 2, 3),
(20, 'REQ-2024-004', '1314-24', '2024-10-05', 100.00, 'Paid', 100.00, NULL, NULL, 'Released', 4, 2, 2),
(21, 'REQ-2024-005', '1327-24', '2024-11-12', 150.00, 'Paid', 150.00, NULL, NULL, 'Released', 2, 2, 3),
(22, 'REQ-2025-001', '1395-24', '2025-01-14', 100.00, 'Paid', 100.00, NULL, NULL, 'Released', 4, 2, 3),
(23, 'REQ-2025-002', '1396-24', '2025-03-22', 150.00, 'Paid', 150.00, NULL, NULL, 'Released', 2, 2, 2),
(24, 'REQ-2025-003', '1522-24', '2025-06-18', 50.00, 'Paid', 50.00, NULL, NULL, 'Processing', 4, 2, NULL),
(25, 'REQ-2025-004', '1808-23', '2025-09-02', 200.00, 'Paid', 200.00, NULL, NULL, 'Cancelled', 2, NULL, NULL),
(26, 'REQ-2025-005', '2055-24', '2025-11-10', 100.00, 'Unpaid', 0.00, NULL, NULL, 'Cancelled', 4, NULL, NULL),
(27, 'REQ-2026-017', '1123-24', '2026-08-05', 150.00, 'Paid', 150.00, 'OR-10011', '2026-08-05', 'Released', 4, 2, 3),
(28, 'REQ-2026-018', '1127-24', '2026-08-12', 50.00, 'Paid', 50.00, 'OR-10012', '2026-08-12', 'Released', 2, 3, 4),
(29, 'REQ-2026-019', '1208-24', '2026-08-18', 200.00, 'Paid', 200.00, 'OR-10013', '2026-08-18', 'Ready for Release', 3, 4, 2),
(30, 'REQ-2026-020', '1314-24', '2026-08-22', 100.00, 'Paid', 100.00, 'OR-10014', '2026-08-22', 'Processing', 4, 2, 3),
(31, 'REQ-2026-021', '1327-24', '2026-08-28', 150.00, 'Paid', 150.00, 'OR-10015', '2026-08-28', 'Ready for Release', 2, 4, 3),
(32, 'REQ-2026-022', '1395-24', '2026-09-02', 100.00, 'Paid', 100.00, 'OR-10016', '2026-09-02', 'Processing', 3, 2, 4),
(33, 'REQ-2026-023', '1396-24', '2026-09-07', 150.00, 'Paid', 150.00, 'OR-10017', '2026-09-07', 'Ready for Release', 4, 3, 2),
(34, 'REQ-2026-024', '1522-24', '2026-09-12', 50.00, 'Paid', 50.00, 'OR-10018', '2026-09-12', 'Processing', 2, 4, 3),
(35, 'REQ-2026-025', '1808-23', '2026-09-16', 200.00, 'Paid', 200.00, 'OR-10019', '2026-09-16', 'Ready for Release', 3, 2, 4),
(36, 'REQ-2026-026', '2055-24', '2026-09-20', 100.00, 'Paid', 100.00, 'OR-10020', '2026-09-20', 'Processing', 4, 3, 2);

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

--
-- Dumping data for table `tblrequestdetails`
--

INSERT INTO `tblrequestdetails` (`RequestDetailID`, `RequestID`, `DocumentID`, `Quantity`, `Amount`, `SubTotal`) VALUES
(1, 16, 4, 1, 50.00, 50.00),
(2, 1, 1, 1, 150.00, 150.00),
(3, 2, 4, 1, 50.00, 50.00),
(4, 3, 4, 1, 100.00, 100.00),
(5, 4, 4, 1, 200.00, 200.00),
(6, 5, 1, 1, 50.00, 50.00),
(7, 6, 5, 1, 100.00, 100.00),
(8, 7, 3, 1, 150.00, 150.00),
(9, 8, 3, 1, 50.00, 50.00),
(10, 9, 3, 1, 250.00, 250.00),
(11, 10, 4, 1, 100.00, 100.00),
(12, 11, 3, 1, 150.00, 150.00),
(13, 12, 3, 1, 50.00, 50.00),
(14, 13, 1, 1, 100.00, 100.00),
(15, 14, 3, 1, 200.00, 200.00),
(16, 15, 3, 1, 50.00, 50.00),
(17, 17, 1, 1, 150.00, 150.00),
(18, 18, 2, 1, 50.00, 50.00),
(19, 19, 3, 1, 200.00, 200.00),
(20, 20, 1, 1, 100.00, 100.00),
(21, 21, 2, 1, 150.00, 150.00),
(22, 22, 1, 1, 100.00, 100.00),
(23, 23, 3, 1, 150.00, 150.00),
(24, 24, 2, 1, 50.00, 50.00),
(25, 25, 1, 1, 200.00, 200.00),
(26, 26, 2, 1, 100.00, 100.00),
(32, 27, 1, 1, 150.00, 150.00),
(33, 28, 2, 1, 50.00, 50.00),
(34, 29, 3, 1, 200.00, 200.00),
(35, 30, 1, 1, 100.00, 100.00),
(36, 31, 2, 1, 150.00, 150.00),
(37, 32, 1, 1, 100.00, 100.00),
(38, 33, 3, 1, 150.00, 150.00),
(39, 34, 2, 1, 50.00, 50.00),
(40, 35, 1, 1, 200.00, 200.00),
(41, 36, 2, 1, 100.00, 100.00);

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
('1123-24', '098760987612', 'Fernandez', 'Gio', 'Natividad', 'Bachelor of Science in Information Technology', '3rd Year', '31E1', '09609829478', 'Active'),
('1127-24', '123456789016', 'Enclona', 'Paul Benedict', '', 'Bachelor of Science in Information Technology', '3rd Year', '31E1', '09123456789', 'Active'),
('1208-24', '123456789018', 'Para', 'Andrea', '', 'Bachelor of Science in Information Technology', '3rd Year', '31E1', '09123456789', 'Active'),
('1314-24', '253435623456', 'Batoy', 'Nicholo John', 'Lopez', 'Bachelor of Science in Information Technology', '3rd Year', '31E1', '09676781233', 'Active'),
('1327-24', '123456789015', 'Reales', 'Jonnidel', 'Paradero', 'Bachelor of Science in Information Technology', '3rd Year', '31E1', '09123456789', 'Active'),
('1395-24', '407321150214', 'Solis', 'Sophia Cassandra', 'Villacorte', 'Bachelor of Science in Information Technology', '3rd Year', '31E1', '09690141523', 'Active'),
('1396-24', '123456123412', 'Mendoza', 'Stephanie', 'Pobar', 'Bachelor of Science in Information Technology', '3rd Year', '31E1', '09612564233', 'Active'),
('1522-24', '234567345678', 'Barcinas', 'Marc Denize', 'Babon', 'Bachelor of Science in Information Technology', '3rd Year', '31E1', '09612564765', 'Active'),
('1808-23', '123456789013', 'Villacorte', 'Joshua', 'Joseph', 'Bachelor of Science in Information Technology', '3rd Year', '31E1', '09123456789', 'Active'),
('2055-24', '123456789012', 'Canua', 'Carl James', 'Prado', 'Bachelor of Science in Information Technology', '3rd Year', '31E3', '09764743381', 'Active'),
('2096-24', '123456789019', 'Ramones', 'Leisbeth', 'Bering', 'Bachelor of Science in Information Technology', '3rd Year', '31E1', '09123456789', 'Active'),
('2194-24', '123456789014', 'Sabesaje', 'Sho Uno', '', 'Bachelor of Science in Information Technology', '3rd Year', '31E1', '09123456789', 'Active'),
('2208-24', '123456789017', 'Eullo', 'John Raven', 'Jandoc', 'Bachelor of Science in Information Technology', '3rd Year', '31E1', '09123456789', 'Active'),
('2786-24', '424515150094', 'Roque', 'Kevin Clerck', 'Alora', 'Bachelor of Science in Information Technology', '3rd Year', '31E1', '09626728466', 'Active'),
('2789-24', '136899110095', 'De Vera', 'Alliyah', 'Garcia', 'Bachelor of Science in Information Technology', '3rd Year', '31E1', '09625632435', 'Active');

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
  MODIFY `RequestID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=37;

--
-- AUTO_INCREMENT for table `tblrequestdetails`
--
ALTER TABLE `tblrequestdetails`
  MODIFY `RequestDetailID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=42;

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
