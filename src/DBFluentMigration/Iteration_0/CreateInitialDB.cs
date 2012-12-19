﻿// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=rharik-desktop\mssqlserver2012;Initial Catalog=MethodFitness_DEV2;Integrated Security=True;`
//     Schema:                 ``
//     Include Views:          `False`

//     Factory Name:          `SqlClientFactory`

using FluentMigrator;

namespace Migrations
{
    [Migration(1)]
    public class CreateInitialDB : Migration
    {
        public override void Up()
        {

            //For Appointment_Client
            Create.Table("Appointment_Client").InSchema("dbo")
                .WithColumn("AppointmentId").AsInt32().NotNullable()
                .WithColumn("ClientId").AsInt32().NotNullable();


            //For Client
            Create.Table("Client").InSchema("dbo")
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("ChangedById").AsInt32().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("FirstName").AsString().Nullable()
                .WithColumn("LastName").AsString().Nullable()
                .WithColumn("BirthDate").AsDateTime().Nullable()
                .WithColumn("StartDate").AsDateTime().Nullable()
                .WithColumn("Email").AsString().Nullable()
                .WithColumn("MobilePhone").AsString().Nullable()
                .WithColumn("SecondaryPhone").AsString().Nullable()
                .WithColumn("Address1").AsString().Nullable()
                .WithColumn("Address2").AsString().Nullable()
                .WithColumn("City").AsString().Nullable()
                .WithColumn("State").AsString().Nullable()
                .WithColumn("ZipCode").AsString().Nullable()
                .WithColumn("Notes").AsString().Nullable()
                .WithColumn("ImageUrl").AsString().Nullable()
                .WithColumn("Source").AsString().Nullable()
                .WithColumn("SourceOther").AsString().Nullable()
                .WithColumn("SessionRatesId").AsInt32().Nullable()
                .WithColumn("CreatedById").AsInt32().Nullable();


        
            //For Company
            Create.Table("Company").InSchema("dbo")
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("ChangedById").AsInt32().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("CreatedById").AsInt32().Nullable();


            //For Location
            Create.Table("Location").InSchema("dbo")
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("ChangedById").AsInt32().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("Address1").AsString().Nullable()
                .WithColumn("Address2").AsString().Nullable()
                .WithColumn("City").AsString().Nullable()
                .WithColumn("State").AsString().Nullable()
                .WithColumn("Zip").AsString().Nullable()
                .WithColumn("CreatedById").AsInt32().Nullable();


            //For Payment
            Create.Table("Payment").InSchema("dbo")
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("ChangedById").AsInt32().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("FullHour").AsInt32().Nullable()
                .WithColumn("FullHourTenPack").AsInt32().Nullable()
                .WithColumn("HalfHour").AsInt32().Nullable()
                .WithColumn("HalfHourTenPack").AsInt32().Nullable()
                .WithColumn("Pair").AsInt32().Nullable()
                .WithColumn("PaymentTotal").AsDouble().Nullable()
                .WithColumn("FullHourPrice").AsDouble().Nullable()
                .WithColumn("FullHourTenPackPrice").AsDouble().Nullable()
                .WithColumn("HalfHourPrice").AsDouble().Nullable()
                .WithColumn("HalfHourTenPackPrice").AsDouble().Nullable()
                .WithColumn("PairPrice").AsDouble().Nullable()
                .WithColumn("ClientId").AsInt32().Nullable()
                .WithColumn("PairTenPack").AsInt32().Nullable()
                .WithColumn("PairTenPackPrice").AsDouble().Nullable()
                .WithColumn("CreatedById").AsInt32().Nullable();


            //For Session
            Create.Table("Session").InSchema("dbo")
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("ChangedById").AsInt32().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("Date").AsDateTime().Nullable()
                .WithColumn("Cost").AsDouble().Nullable()
                .WithColumn("AppointmentType").AsString().Nullable()
                .WithColumn("SessionUsed").AsBoolean().Nullable()
                .WithColumn("TrainerPaid").AsBoolean().Nullable()
                .WithColumn("PurchaseBatchNumber").AsString().Nullable()
                .WithColumn("TrainerCheckNumber").AsInt32().Nullable()
                .WithColumn("InArrears").AsBoolean().Nullable()
                .WithColumn("ClientId").AsInt32().Nullable()
                .WithColumn("AppointmentId").AsInt32().Nullable()
                .WithColumn("TrainerId").AsInt32().Nullable()
                .WithColumn("CreatedById").AsInt32().Nullable();


            //For SessionRates
            Create.Table("SessionRates").InSchema("dbo")
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("ChangedById").AsInt32().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("FullHour").AsDouble().Nullable()
                .WithColumn("HalfHour").AsDouble().Nullable()
                .WithColumn("FullHourTenPack").AsDouble().Nullable()
                .WithColumn("HalfHourTenPack").AsDouble().Nullable()
                .WithColumn("Pair").AsDouble().Nullable()
                .WithColumn("PairTenPack").AsDouble().Nullable()
                .WithColumn("CreatedById").AsInt32().Nullable();


            //For security_Operations
            Create.Table("security_Operations").InSchema("dbo")
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Comment").AsString().Nullable()
                .WithColumn("ParentId").AsInt32().Nullable();

            Create.Index("UQ__security__737584F62645B050").OnTable("security_Operations").InSchema("dbo")

                .OnColumn("EntityId").Ascending()

                .OnColumn("Name").Ascending()

                .WithOptions().Unique();


            //For TrainerClientRate
            Create.Table("TrainerClientRate").InSchema("dbo")
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("ChangedById").AsInt32().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("Percent").AsInt32().Nullable()
                .WithColumn("UserId").AsInt32().Nullable()
                .WithColumn("ClientId").AsInt32().Nullable()
                .WithColumn("TrainerId").AsInt32().Nullable()
                .WithColumn("CreatedById").AsInt32().Nullable();


            //For security_Permissions
            Create.Table("security_Permissions").InSchema("dbo")
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Allow").AsBoolean().NotNullable()
                .WithColumn("Level").AsInt32().NotNullable()
                .WithColumn("OperationId").AsInt32().NotNullable()
                .WithColumn("UserId").AsInt32().Nullable()
                .WithColumn("UsersGroupId").AsInt32().Nullable()
                .WithColumn("Description").AsString().Nullable();


            //For security_UsersGroups
            Create.Table("security_UsersGroups").InSchema("dbo")
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("ParentId").AsInt32().Nullable()
                .WithColumn("Description").AsString().Nullable();

            Create.Index("UQ__security__737584F630C33EC3").OnTable("security_UsersGroups").InSchema("dbo")

                .OnColumn("EntityId").Ascending()

                .OnColumn("Name").Ascending()

                .WithOptions().Unique();


            //For TrainerPayment
            Create.Table("TrainerPayment").InSchema("dbo")
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("ChangedById").AsInt32().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("Total").AsDouble().Nullable()
                .WithColumn("TrainerId").AsInt32().Nullable()
                .WithColumn("CreatedById").AsInt32().Nullable();


            //For TrainerPaymentSessionItem
            Create.Table("TrainerPaymentSessionItem").InSchema("dbo")
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("ChangedById").AsInt32().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("AppointmentCost").AsDouble().Nullable()
                .WithColumn("TrainerPay").AsDouble().Nullable()
                .WithColumn("ClientId").AsInt32().Nullable()
                .WithColumn("AppointmentId").AsInt32().Nullable()
                .WithColumn("TrainerPaymentId").AsInt32().Nullable()
                .WithColumn("CreatedById").AsInt32().Nullable();


            //For security_UsersToUsersGroups
            Create.Table("security_UsersToUsersGroups").InSchema("dbo")
                .WithColumn("GroupId").AsInt32().PrimaryKey().NotNullable()
                .WithColumn("UserId").AsInt32().PrimaryKey().NotNullable();


            //For security_UsersGroupsHierarchy
            Create.Table("security_UsersGroupsHierarchy").InSchema("dbo")
                .WithColumn("ParentGroup").AsInt32().PrimaryKey().NotNullable()
                .WithColumn("ChildGroup").AsInt32().PrimaryKey().NotNullable();


            //For User
            Create.Table("User").InSchema("dbo")
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("type").AsString().NotNullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("ChangedById").AsInt32().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("FirstName").AsString().Nullable()
                .WithColumn("LastName").AsString().Nullable()
                .WithColumn("BirthDate").AsDateTime().Nullable()
                .WithColumn("Email").AsString().Nullable()
                .WithColumn("PhoneMobile").AsString().Nullable()
                .WithColumn("SecondaryPhone").AsString().Nullable()
                .WithColumn("Address1").AsString().Nullable()
                .WithColumn("Address2").AsString().Nullable()
                .WithColumn("City").AsString().Nullable()
                .WithColumn("State").AsString().Nullable()
                .WithColumn("ZipCode").AsString().Nullable()
                .WithColumn("Notes").AsString().Nullable()
                .WithColumn("ImageUrl").AsString().Nullable()
                .WithColumn("UserLoginInfoId").AsInt32().Nullable()
                .WithColumn("Color").AsString().Nullable()
                .WithColumn("ClientRateDefault").AsInt32().Nullable()
                .WithColumn("CreatedById").AsInt32().Nullable();


            //For User_UserRole
            Create.Table("User_UserRole").InSchema("dbo")
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("UserRoleId").AsInt32().NotNullable();


            //For Trainer_Client
            Create.Table("Trainer_Client").InSchema("dbo")
                .WithColumn("TrainerId").AsInt32().NotNullable()
                .WithColumn("ClientId").AsInt32().NotNullable();


            //For UserLoginInfo
            Create.Table("UserLoginInfo").InSchema("dbo")
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("ChangedById").AsInt32().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("LoginName").AsString().Nullable()
                .WithColumn("Password").AsString().Nullable()
                .WithColumn("Salt").AsString().Nullable()
                .WithColumn("CanLogin").AsBoolean().Nullable()
                .WithColumn("LastVisitDate").AsDateTime().Nullable()
                .WithColumn("ByPassToken").AsGuid().Nullable()
                .WithColumn("CreatedById").AsInt32().Nullable();


            //For UserRole
            Create.Table("UserRole").InSchema("dbo")
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("ChangedById").AsInt32().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("CreatedById").AsInt32().Nullable();


            //For Appointment
            Create.Table("Appointment").InSchema("dbo")
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("ChangedById").AsInt32().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("Date").AsDateTime().Nullable()
                .WithColumn("StartTime").AsDateTime().Nullable()
                .WithColumn("EndTime").AsDateTime().Nullable()
                .WithColumn("AppointmentType").AsString().Nullable()
                .WithColumn("Completed").AsBoolean().Nullable()
                .WithColumn("LocationId").AsInt32().Nullable()
                .WithColumn("TrainerId").AsInt32().Nullable()
                .WithColumn("CreatedById").AsInt32().Nullable();




            //Foreign Key List 
            Create.ForeignKey("FK_Clients_manyToMany_Appointment").FromTable("Appointment_Client").InSchema("dbo").ForeignColumns("AppointmentId").ToTable("Appointment").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_Clients_manyToMany_Appointment_otherFK").FromTable("Appointment_Client").InSchema("dbo").ForeignColumns("ClientId").ToTable("Client").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_Client_manyToOne_SessionRates").FromTable("Client").InSchema("dbo").ForeignColumns("SessionRatesId").ToTable("SessionRates").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_Client_manyToOne_ChangedById").FromTable("Client").InSchema("dbo").ForeignColumns("ChangedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_Client_manyToOne_CreatedById").FromTable("Client").InSchema("dbo").ForeignColumns("CreatedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_Company_manyToOne_ChangedById").FromTable("Company").InSchema("dbo").ForeignColumns("ChangedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_Company_manyToOne_CreatedById").FromTable("Company").InSchema("dbo").ForeignColumns("CreatedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_Location_manyToOne_ChangedById").FromTable("Location").InSchema("dbo").ForeignColumns("ChangedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_Location_manyToOne_CreatedById").FromTable("Location").InSchema("dbo").ForeignColumns("CreatedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_Payments_oneToMany_Client").FromTable("Payment").InSchema("dbo").ForeignColumns("ClientId").ToTable("Client").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_Payment_manyToOne_ChangedById").FromTable("Payment").InSchema("dbo").ForeignColumns("ChangedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_Payment_manyToOne_CreatedById").FromTable("Payment").InSchema("dbo").ForeignColumns("CreatedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_Sessions_oneToMany_Appointment").FromTable("Session").InSchema("dbo").ForeignColumns("AppointmentId").ToTable("Appointment").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_Sessions_oneToMany_Client").FromTable("Session").InSchema("dbo").ForeignColumns("ClientId").ToTable("Client").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_Session_manyToOne_ChangedById").FromTable("Session").InSchema("dbo").ForeignColumns("ChangedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_Session_manyToOne_CreatedById").FromTable("Session").InSchema("dbo").ForeignColumns("CreatedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_Sessions_oneToMany_Trainer").FromTable("Session").InSchema("dbo").ForeignColumns("TrainerId").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_SessionRates_manyToOne_ChangedById").FromTable("SessionRates").InSchema("dbo").ForeignColumns("ChangedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_SessionRates_manyToOne_CreatedById").FromTable("SessionRates").InSchema("dbo").ForeignColumns("CreatedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKE58BBFF82B7CDCD3").FromTable("security_Operations").InSchema("dbo").ForeignColumns("ParentId").ToTable("security_Operations").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_TrainerClientRate_manyToOne_Client").FromTable("TrainerClientRate").InSchema("dbo").ForeignColumns("ClientId").ToTable("Client").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_TrainerClientRate_manyToOne_ChangedById").FromTable("TrainerClientRate").InSchema("dbo").ForeignColumns("ChangedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_TrainerClientRate_manyToOne_CreatedById").FromTable("TrainerClientRate").InSchema("dbo").ForeignColumns("CreatedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_TrainerClientRate_manyToOne_User").FromTable("TrainerClientRate").InSchema("dbo").ForeignColumns("UserId").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_TrainerClientRates_oneToMany_Trainer").FromTable("TrainerClientRate").InSchema("dbo").ForeignColumns("TrainerId").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKEA223C4C71C937C7").FromTable("security_Permissions").InSchema("dbo").ForeignColumns("OperationId").ToTable("security_Operations").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKEA223C4C2EE8F612").FromTable("security_Permissions").InSchema("dbo").ForeignColumns("UsersGroupId").ToTable("security_UsersGroups").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKEA223C4CFC8C2B95").FromTable("security_Permissions").InSchema("dbo").ForeignColumns("UserId").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKEC3AF233D0CB87D0").FromTable("security_UsersGroups").InSchema("dbo").ForeignColumns("ParentId").ToTable("security_UsersGroups").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_TrainerPayment_manyToOne_ChangedById").FromTable("TrainerPayment").InSchema("dbo").ForeignColumns("ChangedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_TrainerPayment_manyToOne_CreatedById").FromTable("TrainerPayment").InSchema("dbo").ForeignColumns("CreatedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_TrainerPayments_oneToMany_Trainer").FromTable("TrainerPayment").InSchema("dbo").ForeignColumns("TrainerId").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_TrainerPaymentSessionItem_manyToOne_Appointment").FromTable("TrainerPaymentSessionItem").InSchema("dbo").ForeignColumns("AppointmentId").ToTable("Appointment").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_TrainerPaymentSessionItem_manyToOne_Client").FromTable("TrainerPaymentSessionItem").InSchema("dbo").ForeignColumns("ClientId").ToTable("Client").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_TrainerPaymentSessionItems_oneToMany_TrainerPayment").FromTable("TrainerPaymentSessionItem").InSchema("dbo").ForeignColumns("TrainerPaymentId").ToTable("TrainerPayment").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_TrainerPaymentSessionItem_manyToOne_ChangedById").FromTable("TrainerPaymentSessionItem").InSchema("dbo").ForeignColumns("ChangedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_TrainerPaymentSessionItem_manyToOne_CreatedById").FromTable("TrainerPaymentSessionItem").InSchema("dbo").ForeignColumns("CreatedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK7817F27A1238D4D4").FromTable("security_UsersToUsersGroups").InSchema("dbo").ForeignColumns("GroupId").ToTable("security_UsersGroups").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK7817F27AA6C99102").FromTable("security_UsersToUsersGroups").InSchema("dbo").ForeignColumns("UserId").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK69A3B61FA860AB70").FromTable("security_UsersGroupsHierarchy").InSchema("dbo").ForeignColumns("ChildGroup").ToTable("security_UsersGroups").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK69A3B61FA87BAE50").FromTable("security_UsersGroupsHierarchy").InSchema("dbo").ForeignColumns("ParentGroup").ToTable("security_UsersGroups").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_User_manyToOne_ChangedById").FromTable("User").InSchema("dbo").ForeignColumns("ChangedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_User_manyToOne_CreatedById").FromTable("User").InSchema("dbo").ForeignColumns("CreatedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_User_manyToOne_UserLoginInfo").FromTable("User").InSchema("dbo").ForeignColumns("UserLoginInfoId").ToTable("UserLoginInfo").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_UserRoles_manyToMany_User").FromTable("User_UserRole").InSchema("dbo").ForeignColumns("UserId").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_UserRoles_manyToMany_User_otherFK").FromTable("User_UserRole").InSchema("dbo").ForeignColumns("UserRoleId").ToTable("UserRole").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_Clients_manyToMany_Trainer_otherFK").FromTable("Trainer_Client").InSchema("dbo").ForeignColumns("ClientId").ToTable("Client").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_Clients_manyToMany_Trainer").FromTable("Trainer_Client").InSchema("dbo").ForeignColumns("TrainerId").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_UserLoginInfo_manyToOne_ChangedById").FromTable("UserLoginInfo").InSchema("dbo").ForeignColumns("ChangedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_UserLoginInfo_manyToOne_CreatedById").FromTable("UserLoginInfo").InSchema("dbo").ForeignColumns("CreatedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_UserRole_manyToOne_ChangedById").FromTable("UserRole").InSchema("dbo").ForeignColumns("ChangedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_UserRole_manyToOne_CreatedById").FromTable("UserRole").InSchema("dbo").ForeignColumns("CreatedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_Appointment_manyToOne_Location").FromTable("Appointment").InSchema("dbo").ForeignColumns("LocationId").ToTable("Location").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_Appointment_manyToOne_ChangedById").FromTable("Appointment").InSchema("dbo").ForeignColumns("ChangedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_methodFit_Appointment_manyToOne_CreatedById").FromTable("Appointment").InSchema("dbo").ForeignColumns("CreatedById").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_Appointment_manyToOne_Trainer").FromTable("Appointment").InSchema("dbo").ForeignColumns("TrainerId").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_Clients_manyToMany_Appointment"); Delete.ForeignKey("FK_Clients_manyToMany_Appointment_otherFK"); Delete.ForeignKey("FK_Client_manyToOne_SessionRates"); Delete.ForeignKey("FK_methodFit_Client_manyToOne_ChangedById"); Delete.ForeignKey("FK_methodFit_Client_manyToOne_CreatedById"); Delete.ForeignKey("FK_methodFit_Company_manyToOne_ChangedById"); Delete.ForeignKey("FK_methodFit_Company_manyToOne_CreatedById"); Delete.ForeignKey("FK_methodFit_Location_manyToOne_ChangedById"); Delete.ForeignKey("FK_methodFit_Location_manyToOne_CreatedById"); Delete.ForeignKey("FK_Payments_oneToMany_Client"); Delete.ForeignKey("FK_methodFit_Payment_manyToOne_ChangedById"); Delete.ForeignKey("FK_methodFit_Payment_manyToOne_CreatedById"); Delete.ForeignKey("FK_Sessions_oneToMany_Appointment"); Delete.ForeignKey("FK_Sessions_oneToMany_Client"); Delete.ForeignKey("FK_methodFit_Session_manyToOne_ChangedById"); Delete.ForeignKey("FK_methodFit_Session_manyToOne_CreatedById"); Delete.ForeignKey("FK_Sessions_oneToMany_Trainer"); Delete.ForeignKey("FK_methodFit_SessionRates_manyToOne_ChangedById"); Delete.ForeignKey("FK_methodFit_SessionRates_manyToOne_CreatedById"); Delete.ForeignKey("FKE58BBFF82B7CDCD3"); Delete.ForeignKey("FK_TrainerClientRate_manyToOne_Client"); Delete.ForeignKey("FK_methodFit_TrainerClientRate_manyToOne_ChangedById"); Delete.ForeignKey("FK_methodFit_TrainerClientRate_manyToOne_CreatedById"); Delete.ForeignKey("FK_TrainerClientRate_manyToOne_User"); Delete.ForeignKey("FK_TrainerClientRates_oneToMany_Trainer"); Delete.ForeignKey("FKEA223C4C71C937C7"); Delete.ForeignKey("FKEA223C4C2EE8F612"); Delete.ForeignKey("FKEA223C4CFC8C2B95"); Delete.ForeignKey("FKEC3AF233D0CB87D0"); Delete.ForeignKey("FK_methodFit_TrainerPayment_manyToOne_ChangedById"); Delete.ForeignKey("FK_methodFit_TrainerPayment_manyToOne_CreatedById"); Delete.ForeignKey("FK_TrainerPayments_oneToMany_Trainer"); Delete.ForeignKey("FK_TrainerPaymentSessionItem_manyToOne_Appointment"); Delete.ForeignKey("FK_TrainerPaymentSessionItem_manyToOne_Client"); Delete.ForeignKey("FK_TrainerPaymentSessionItems_oneToMany_TrainerPayment"); Delete.ForeignKey("FK_methodFit_TrainerPaymentSessionItem_manyToOne_ChangedById"); Delete.ForeignKey("FK_methodFit_TrainerPaymentSessionItem_manyToOne_CreatedById"); Delete.ForeignKey("FK7817F27A1238D4D4"); Delete.ForeignKey("FK7817F27AA6C99102"); Delete.ForeignKey("FK69A3B61FA860AB70"); Delete.ForeignKey("FK69A3B61FA87BAE50"); Delete.ForeignKey("FK_methodFit_User_manyToOne_ChangedById"); Delete.ForeignKey("FK_methodFit_User_manyToOne_CreatedById"); Delete.ForeignKey("FK_User_manyToOne_UserLoginInfo"); Delete.ForeignKey("FK_UserRoles_manyToMany_User"); Delete.ForeignKey("FK_UserRoles_manyToMany_User_otherFK"); Delete.ForeignKey("FK_Clients_manyToMany_Trainer_otherFK"); Delete.ForeignKey("FK_Clients_manyToMany_Trainer"); Delete.ForeignKey("FK_methodFit_UserLoginInfo_manyToOne_ChangedById"); Delete.ForeignKey("FK_methodFit_UserLoginInfo_manyToOne_CreatedById"); Delete.ForeignKey("FK_methodFit_UserRole_manyToOne_ChangedById"); Delete.ForeignKey("FK_methodFit_UserRole_manyToOne_CreatedById"); Delete.ForeignKey("FK_Appointment_manyToOne_Location"); Delete.ForeignKey("FK_methodFit_Appointment_manyToOne_ChangedById"); Delete.ForeignKey("FK_methodFit_Appointment_manyToOne_CreatedById"); Delete.ForeignKey("FK_Appointment_manyToOne_Trainer");

            Delete.Index("UQ__security__737584F62645B050");

            Delete.Index("UQ__security__737584F630C33EC3");



            Delete.Table("Appointment");

            Delete.Table("UserRole");

            Delete.Table("UserLoginInfo");

            Delete.Table("Trainer_Client");

            Delete.Table("User_UserRole");

            Delete.Table("User");

            Delete.Table("security_UsersGroupsHierarchy");

            Delete.Table("security_UsersToUsersGroups");

            Delete.Table("TrainerPaymentSessionItem");

            Delete.Table("TrainerPayment");

            Delete.Table("security_UsersGroups");

            Delete.Table("security_Permissions");

            Delete.Table("TrainerClientRate");

            Delete.Table("security_Operations");

            Delete.Table("SessionRates");

            Delete.Table("Session");

            Delete.Table("Payment");

            Delete.Table("Location");

            Delete.Table("Company");

            Delete.Table("Client");

            Delete.Table("Appointment_Client");

        }
    }
}


