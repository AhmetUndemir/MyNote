﻿
namespace MyEvernote.Entities.Messages
{
	public enum ErrorMessageCode
	{
		UsernameAlreadyExists = 101,
		EmailAlreadyExists = 102,
		UserIsNotActive = 151,
		UsernameOrPasswrong = 152,
		CheckYourEmail = 153,
		UserAlreadyActive = 154,
		ActivateIdDoesNotExists = 155,
		UserNotFound = 156,
        ProfileCouldNotUpdated = 157,
        UserCouldNotRemove = 158,
        UserCouldNotFind = 159
    }
}
