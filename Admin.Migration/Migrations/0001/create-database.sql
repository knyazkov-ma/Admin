    
    create table TypeWorkerUser (
        Id BIGINT not null,
       Name VARCHAR(200) not null,
       primary key (Id)
    )

    create table WorkerUser (
        Id BIGINT not null,
       Name VARCHAR(200) not null,
       Email VARCHAR(200) not null unique,
       Password VARCHAR(100) not null,
       Disabled BIT not null,
       TypeWorkerUserId BIGINT not null,
       primary key (Id)
    )

    create index User_Email_idx on WorkerUser (Email)

    alter table WorkerUser 
        add constraint WorkerUser_TypeWorkerUser_FK 
        foreign key (TypeWorkerUserId) 
        references TypeWorkerUser
    
    create sequence SQ_GLOBAL as int start with 100000 increment by 1


insert into TypeWorkerUser(Id, Name) values(1,'Администратор системы');
insert into TypeWorkerUser(Id, Name) values(2,'Администратор пациентов');
insert into TypeWorkerUser(Id, Name) values(3,'Маркетолог');

/*суперадмин*/
insert into WorkerUser(Id, Email, Password, Name, TypeWorkerUserId, Disabled) values(1, 'admin@admin.ru',	'admin', 'Суперпользователь',	1, 0);
