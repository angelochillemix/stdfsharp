/**
 * $Id: MirRecord.cs 20 2008-06-08 07:05:10Z outburst $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: MirRecord.cs
 * Description:
 * 
 * Copyright (C) 2006 Outburst <outburst@users.sourceforge.net>
 *  
 * This library is free software; you can redistribute it and/or 
 * modify it under the terms of the GNU Lesser General Public License 
 * as published by the Free Software Foundation; either version 2.1 
 * of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful, 
 * but WITHOUT ANY WARRANTY; without even the implied warranty of 
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
 * GNU Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public License
 * along with this library; if not, write to the Free Software Foundation, 
 * Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
 */

using System;
using KA.StdfSharp.Record.Field;

namespace KA.StdfSharp.Record
{
    /// <summary>
    /// Represents the MIR record of STDF
    /// </summary>
    [StdfRecord(1, 10)]
    public sealed class MirRecord : StdfRecord
    {
        private IField<DateTime>    jobSetupDate                    = new StdfDate(); // SETUP_T U*4 Date and time of job setup
        private IField<DateTime>    firstPartTestedDate             = new StdfDate(); // START_T U*4 Date and time first part tested
        private IField<byte>        testerStationNumber             = new StdfUByte(); // STAT_NUM U*1 Tester station number
        private IField<char>        testModeCode                    = new StdfChar(); // MODE_COD C*1 Test mode code (e.g. prod, dev)
        private IField<char[]>      lotRetestCode                   = new FixedLengthCharacter(1); // RTST_COD C*1 Lot retest code
        private IField<char[]>      dataProtectionCode              = new FixedLengthCharacter(1); // PROT_COD C*1 Data protection code
        private IField<ushort>      burnTime                        = new StdfUShort(); // BURN_TIM U*2 Burn-in time (in minutes) 
        private IField<char>        commandModeCode                 = new StdfChar(); // CMOD_COD C*1 Command mode code space
        private IField<string>      lotId                           = new StdfString(); // LOT_ID C*n Lot ID (customer specified)
        private IField<string>      partType                        = new StdfString(); // PART_TYP C*n Part Type (or product ID)
        private IField<string>      nodeName                        = new StdfString(); // NODE_NAM C*n Name of node that generated data
        private IField<string>      testerType                      = new StdfString(); // TSTR_TYP C*n Tester type
        private IField<string>      jobName                         = new StdfString(); // JOB_NAM C*n Job name (test program name)
        private IField<string>      jobRevision                     = new StdfString(); // JOB_REV C*n Job (test program) revision number 
        private IField<string>      subLotId                        = new StdfString(); // SBLOT_ID C*n Sublot ID 
        private IField<string>      operatorName                    = new StdfString(); // OPER_NAM C*n Operator name or ID (at setup time) 
        private IField<string>      testerExecutiveSoftwareType     = new StdfString(); // EXEC_TYP C*n Tester executive software type
        private IField<string>      testerExecutiveSoftwareVersion  = new StdfString(); // EXEC_VER C*n Tester exec software version number
        private IField<string>      testPhaseCode                   = new StdfString(); // TEST_COD C*n Test phase or step code
        private IField<string>      testTemperature                 = new StdfString(); // TST_TEMP C*n Test temperature
        private IField<string>      userText                        = new StdfString(); // USER_TXT C*n Generic user text
        private IField<string>      auxiliaryDataFile               = new StdfString(); // AUX_FILE C*n Name of auxiliary data file length
        private IField<string>      packageType                     = new StdfString(); // PKG_TYP C*n Package type length
        private IField<string>      familyId                        = new StdfString(); // FAMLY_ID C*n Product family ID 
        private IField<string>      dateCode                        = new StdfString(); // DATE_COD C*n Date code
        private IField<string>      testFacilityId                  = new StdfString(); // FACIL_ID C*n Test facility ID 
        private IField<string>      testFloorId                     = new StdfString(); // FLOOR_ID C*n Test floor ID length byte = 0
        private IField<string>      fabricationProcessId            = new StdfString(); // PROC_ID C*n Fabrication process ID 
        private IField<string>      operationFrequency              = new StdfString(); // OPER_FRQ C*n Operation frequency or step
        private IField<string>      testSpecificationName           = new StdfString(); // SPEC_NAM C*n Test specification name
        private IField<string>      testSpecificationVersion        = new StdfString(); // SPEC_VER C*n Test specification version number
        private IField<string>      testFlowId                      = new StdfString(); // FLOW_ID C*n Test flow ID
        private IField<string>      testSetupId                     = new StdfString(); // SETUP_ID C*n Test setup ID 
        private IField<string>      deviceDesignRevision            = new StdfString(); // DSGN_REV C*n Device design revision 
        private IField<string>      engineeringLotId                = new StdfString(); // ENG_ID C*n Engineering lot ID 
        private IField<string>      romCodeId                       = new StdfString(); // ROM_COD C*n ROM code ID 
        private IField<string>      testerSerialNumber              = new StdfString(); // SERL_NUM C*n Tester serial number 
        private IField<string>      supervisorName                  = new StdfString(); // SUPR_NAM C*n Supervisor name or ID

        public enum FieldName
        {
            SETUP_T,
            START_T,
            STAT_NUM,
            MODE_COD,
            RTST_COD,
            PROT_COD,
            BURN_TIM,
            CMOD_COD,
            LOT_ID,
            PART_TYP,
            NODE_NAM,
            TSTR_TYP,
            JOB_NAM,
            JOB_REV,
            SBLOT_ID,
            OPER_NAM,
            EXEC_TYP,
            EXEC_VER,
            TEST_COD,
            TST_TEMP,
            USER_TXT,
            AUX_FILE,
            PKG_TYP,
            FAMLY_ID,
            DATE_COD,
            FACIL_ID,
            FLOOR_ID,
            PROC_ID,
            OPER_FRQ,
            SPEC_NAM,
            SPEC_VER,
            FLOW_ID,
            SETUP_ID,
            DSGN_REV,
            ENG_ID,
            ROM_COD,
            SERL_NUM,
            SUPR_NAM
        }
        
        public MirRecord()
        {
            AddField(FieldName.SETUP_T.ToString(), jobSetupDate);
            AddField(FieldName.START_T.ToString(), firstPartTestedDate);
            AddField(FieldName.STAT_NUM.ToString(), testerStationNumber);
            AddField(FieldName.MODE_COD.ToString(), testModeCode);
            AddField(FieldName.RTST_COD.ToString(), lotRetestCode);
            AddField(FieldName.PROT_COD.ToString(), dataProtectionCode);
            AddField(FieldName.BURN_TIM.ToString(), burnTime);
            AddField(FieldName.CMOD_COD.ToString(), commandModeCode);
            AddField(FieldName.LOT_ID.ToString(), lotId);
            AddField(FieldName.PART_TYP.ToString(), partType);
            AddField(FieldName.NODE_NAM.ToString(), nodeName);
            AddField(FieldName.TSTR_TYP.ToString(), testerType);
            AddField(FieldName.JOB_NAM.ToString(), jobName);
            AddField(FieldName.JOB_REV.ToString(), jobRevision);
            AddField(FieldName.SBLOT_ID.ToString(), subLotId);
            AddField(FieldName.OPER_NAM.ToString(), operatorName);
            AddField(FieldName.EXEC_TYP.ToString(), testerExecutiveSoftwareType);
            AddField(FieldName.EXEC_VER.ToString(), testerExecutiveSoftwareVersion);
            AddField(FieldName.TEST_COD.ToString(), testPhaseCode);
            AddField(FieldName.TST_TEMP.ToString(), testTemperature);
            AddField(FieldName.USER_TXT.ToString(), userText);
            AddField(FieldName.AUX_FILE.ToString(), auxiliaryDataFile);
            AddField(FieldName.PKG_TYP.ToString(), packageType);
            AddField(FieldName.FAMLY_ID.ToString(), familyId);
            AddField(FieldName.DATE_COD.ToString(), dateCode);
            AddField(FieldName.FACIL_ID.ToString(), testFacilityId);
            AddField(FieldName.FLOOR_ID.ToString(), testFloorId);
            AddField(FieldName.PROC_ID.ToString(), fabricationProcessId);
            AddField(FieldName.OPER_FRQ.ToString(), operationFrequency);
            AddField(FieldName.SPEC_NAM.ToString(), testSpecificationName);
            AddField(FieldName.SPEC_VER.ToString(), testSpecificationVersion);
            AddField(FieldName.FLOW_ID.ToString(), testFlowId);
            AddField(FieldName.SETUP_ID.ToString(), testSetupId);
            AddField(FieldName.DSGN_REV.ToString(), deviceDesignRevision);
            AddField(FieldName.ENG_ID.ToString(), engineeringLotId);
            AddField(FieldName.ROM_COD.ToString(), romCodeId);
            AddField(FieldName.SERL_NUM.ToString(), testerSerialNumber);
            AddField(FieldName.SUPR_NAM.ToString(), supervisorName);
        }

        public IField<DateTime> JobSetupDate
        {
            get { return jobSetupDate; }
        }

        public IField<DateTime> FirstPartTestedDate
        {
            get { return firstPartTestedDate; }
        }

        public IField<byte> TesterStationNumber
        {
            get { return testerStationNumber; }
        }

        public IField<char> TestModeCode
        {
            get { return testModeCode; }
        }

        public IField<char[]> LotRetestCode
        {
            get { return lotRetestCode; }
        }

        public IField<char[]> DataProtectionCode
        {
            get { return dataProtectionCode; }
        }

        public IField<ushort> BurnTime
        {
            get { return burnTime; }
        }

        public IField<char> CommandModeCode
        {
            get { return commandModeCode; }
        }

        public IField<string> LotId
        {
            get { return lotId; }
        }

        public IField<string> PartType
        {
            get { return partType; }
        }

        public IField<string> NodeName
        {
            get { return nodeName; }
        }

        public IField<string> TesterType
        {
            get { return testerType; }
        }

        public IField<string> JobName
        {
            get { return jobName; }
        }

        public IField<string> JobRevision
        {
            get { return jobRevision; }
        }

        public IField<string> SubLotId
        {
            get { return subLotId; }
        }

        public IField<string> OperatorName
        {
            get { return operatorName; }
        }

        public IField<string> TesterExecutiveSoftwareType
        {
            get { return testerExecutiveSoftwareType; }
        }

        public IField<string> TesterExecutiveSoftwareVersion
        {
            get { return testerExecutiveSoftwareVersion; }
        }

        public IField<string> TestPhaseCode
        {
            get { return testPhaseCode; }
        }

        public IField<string> TestTemperature
        {
            get { return testTemperature; }
        }

        public IField<string> UserText
        {
            get { return userText; }
        }

        public IField<string> AuxiliaryDataFile
        {
            get { return auxiliaryDataFile; }
        }

        public IField<string> PackageType
        {
            get { return packageType; }
        }

        public IField<string> FamilyId
        {
            get { return familyId; }
        }

        public IField<string> DateCode
        {
            get { return dateCode; }
        }

        public IField<string> TestFacilityId
        {
            get { return testFacilityId; }
        }

        public IField<string> TestFloorId
        {
            get { return testFloorId; }
        }

        public IField<string> FabricationProcessId
        {
            get { return fabricationProcessId; }
        }

        public IField<string> OperationFrequency
        {
            get { return operationFrequency; }
        }

        public IField<string> TestSpecificationName
        {
            get { return testSpecificationName; }
        }

        public IField<string> TestSpecificationVersion
        {
            get { return testSpecificationVersion; }
        }

        public IField<string> TestFlowId
        {
            get { return testFlowId; }
        }

        public IField<string> TestSetupId
        {
            get { return testSetupId; }
        }

        public IField<string> DeviceDesignRevision
        {
            get { return deviceDesignRevision; }
        }

        public IField<string> EngineeringLotId
        {
            get { return engineeringLotId; }
        }

        public IField<string> RomCodeId
        {
            get { return romCodeId; }
        }

        public IField<string> TesterSerialNumber
        {
            get { return testerSerialNumber; }
        }

        public IField<string> SupervisorName
        {
            get { return supervisorName; }
        }
   }
}