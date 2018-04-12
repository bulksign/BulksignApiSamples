namespace Bulksign {
	public static class ApiErrorCode {
		public const int API_CODE_SUCCESS = 0;

		public const int API_ERROR_CODE_INTERNAL = -1;
		public const string API_MESSAGE_INTERNAL = "Internal error";

		public const int API_ERROR_CODE_MISSING_AUTHENTICATION = 2;
		public const string API_MESSAGE_MISSING_AUTHENTICATION = "Authentication header is missing";

		public const int API_ERROR_CODE_INVALID_AUTHENTICATION = 3;
		public const string API_MESSAGE_INVALID_AUTHENTICATION = "Invalid authentication details";

		public const int API_ERROR_CODE_INVALID_INPUT = 4;
		public const string API_MESSAGE_INVALID_INPUT = "Input is invalid. Please check all passed values to make sure they are correct. ";

		public const int API_ERROR_CODE_OPERATION_FAILED = 5;
		public const string API_MESSAGE_OPERATION_FAILED = "Operation failed";

		public const int API_ERROR_CODE_NO_ACCESS = 6;
		public const string API_MESSAGE_NO_ACCESS = "No access to ";

		public const int API_ERROR_CODE_NEW_DRAFT_TEMPLATE_LICENSE = 20;
		public const string API_MESSAGE_NEW_DRAFT_TEMPLATE_LICENSE = "Your subscription doesn't allow you to create new drafts/templates. Please upgrade your subscription to create new drafts/templates";

		public const int API_ERROR_CODE_NO_DOCUMENTS_AND_RECIPIENTS = 21;
		public const string API_MESSAGE_NO_DOCUMENTS_AND_RECIPIENTS = "No recipients or documents specified. Invalid input";

		public const int API_ERROR_CODE_NO_REDIRECTION_URL = 22;
		public const string API_MESSAGE_NO_REDIRECTION_URL = "Your subscription doesn\'t allow you to have client redirection urls. Please upgrade your subscription to have access to this feature";

		public const int API_ERROR_CODE_WRONG_NOTIFICATION_LANGUAGE = 23;

		public const int API_ERROR_CODE_EMAIL_SUBJECT_LENGTH = 24;
		public const int API_ERROR_CODE_EMAIL_BODY_LENGTH = 299;

		public const int API_ERROR_CODE_INVALID_FILE_NAME = 25;
		public const string API_MESSAGE_INVALID_FILE_NAME = "Document filename is required";

		public const int API_ERROR_CODE_BUNDLE_MUST_BE_COMPLETED_STATE = 26;
		public const string API_MESSAGE_BUNDLE_MUST_BE_COMPLETED_STATE = "Specified bundle must be in completed state";

		public const int API_ERROR_CODE_RECIPIENT_EMAIL_NOT_FOUND = 27;
		public const string API_MESSAGE_RECIPIENT_EMAIL_NOT_FOUND = "Recipient with specified email was not found in bundle with id ";

		public const int API_ERROR_CODE_BUNDLE_STEP_RECIPIENT_EMAIL_NOT_FOUND = 28;
		public const string API_MESSAGE_BUNDLE_STEP_RECIPIENT_EMAIL_NOT_FOUND = "Bundle step not found for recipient with email ";

		public const int API_ERROR_CODE_BUNDLE_MUST_BE_INPROGRESS_STATE = 29;
		public const string API_MESSAGE_BUNDLE_MUST_BE_INPROGRESS_STATE = "The specified bundle must have status InProgress to perform the required action";

		public const int API_ERROR_CODE_NEW_BUNDLE_LICENSE = 30;
		public const string API_MESSAGE_NEW_BUNDLE_LICENSE = "Your subscription doesn't allow you to create new bundles. Please upgrade your subscription to send new bundles";

		public const int API_ERROR_CODE_INVALID_CONTENT_FILE = 31;
		public const string API_MESSAGE_INVALID_CONTENT_FILE = "Invalid file, content is empty";

		public const int API_ERROR_CODE_BULK_BUNDLE_INVALID_NUMBER_RECIPIENTS = 31;
		public const string API_MESSAGE_BULK_BUNDLE_INVALID_NUMBER_RECIPIENTS = "Bulk bundles must have at least 2 recipients. If you want to send a bundle to a single recipient please call the SendBundle API method.";

		public const int API_ERROR_CODE_BULK_NON_SIGNING_RECIPIENTS = 32;
		public const string API_MESSAGE_BULK_NON_SIGNING_RECIPIENTS = "Cannot have non signing recipients in bulk bundle";

		public const int API_ERROR_CODE_BULK_MULTIPLE_RECIPIENTS_SAME_INDEX = 33;
		public const string API_MESSAGE_BULK_MULTIPLE_RECIPIENTS_SAME_INDEX = "Multiple recipients have the same index";

		public const int API_ERROR_CODE_NO_RECIPIENT_AUTH_PASSWORD = 34;
		public const string API_MESSAGE_NO_RECIPIENT_AUTH_PASSWORD = "No authorization password set for recipient ";

		public const int API_ERROR_CODE_PASSWORD_TOO_LONG = 35;
		public const string API_MESSAGE_PASSWORD_TOO_LONG = "Password for recipient {0} is too long. Max length is {1}";

		public const int API_ERROR_CODE_INVALID_LANGUAGE = 36;
		public const string API_MESSAGE_INVALID_LANGUAGE = "Language {0} not found, is not among the usable notification languages in your organization. Either leave field empty for default language or pass a valid language (en-US for example)";

		public const int API_ERROR_CODE_INVALID_REMINDER_EXPIRATION_DAY = 37;
		public const string API_MESSAGE_INVALID_REMINDER_EXPIRATION_DAY = "Reminder expiration day value value must be between 1 and {0}";

		public const int API_ERROR_CODE_INVALID_RECURRENT_REMINDER = 38;
		public const string API_MESSAGE_INVALID_RECURRENT_REMINDER = "Reminder expiration day value value must be between 1 and {0}";

		public const int API_ERROR_CODE_INVALID_EXPIRATION_DATE = 39;
		public const string API_MESSAGE_INVALID_EXPIRATION_DATE = "Invalid expiration date. Can't be bigger than {0} days";

		public const int API_ERROR_CODE_ANALYZE_FILE_GENERIC = 40;
		public const string API_MESSAGE_ANALYZE_FILE_GENERIC = "Error occurred while analyzing the file. Make sure file is not password protected";

		public const int API_ERROR_CODE_FILE_BASE64_ENCODING = 46;
		public const string API_MESSAGE_FILE_BASE64_ENCODING = "Invalid input. Must be base64 encoded file content";

		public const int API_ERROR_CODE_NOTIFICATION_DISABLED_REMINDERS_ENABLED = 47;
		public const string API_MESSAGE_NOTIFICATION_DISABLED_REMINDERS_ENABLED = "Both DisableNotifications and RemindersEnabled are true. No point in having reminders enabled if notifications are disabled. Please choose one of them";

		public const int API_ERROR_CODE_NOTIFICATION_NULL = 48;
		public const string API_MESSAGE_NOTIFICATION_NULL = "Notifications are enabled but NotificationOptions is null";

		public const int API_ERROR_CODE_FILE_CONTENT_EMPTY = 49;
		public const string API_MESSAGE_FILE_CONTENT_EMPTY = "{0} content is empty";

		public const int API_ERROR_CODE_FILE_CONTENT_BOTH_VALUES = 50;
		public const string API_MESSAGE_FILE_CONTENT_BOTH_VALUES = "{0} content has values in both ContentAsBase64String and ContentBytes. Please set the value of only one of them";

		public const int API_ERROR_CODE_VALIDATION_FAILED = 99;

		public const int API_ERROR_CODE_INVALID_BUNLDESTEP_ID = 200;
		public const string API_MESSAGE_INVALID_BUNLDESTEP_ID = "Invalid id";

		public const int API_ERROR_CODE_INVALID_BUNLDESTEP_STATUS = 201;
		public const string API_MESSAGE_INVALID_BUNLDESTEP_STATUS = "Invalid bundlestep status";

		public const int API_ERROR_CODE_BUNDLESTEP_INVALID_SIG_DISCLOSURE = 202;

		public const int API_ERROR_CODE_BUNDLESTEP_ACCEPTED_SIG_DISCLOSURE = 203;

		public const int API_ERROR_CODE_BUNDLESTEP_NO_SIG_DISCLOSURE = 204;

		public const int API_ERROR_CODE_BUNDLESTEP_AUTH_NO_PASSWORD = 205;
		public const string API_MESSAGE_BUNDLESTEP_AUTH_NO_PASSWORD = "No password authentification supported";

		public const int API_ERROR_CODE_MAX_ATTACHMENT_FILE_TOO_BIG = 206;

		public const int API_ERROR_CODE_INVALID_PASSWORD_AUTHENTICATION = 207;
		public const string API_MESSAGE_INVALID_PASSWORD_AUTHENTICATION = "Invalid password";

		public const int API_ERROR_CODE_INVALID_PIN_AUTHENTICATION = 208;
		public const string API_MESSAGE_INVALID_PIN_AUTHENTICATION = "Invalid pin";

		public const int API_ERROR_CODE_BUNDLE_STEP_INVALID_STATE_AUDIT_TRAIL = 209;
		public const string API_MESSAGE_BUNDLE_STEP_INVALID_STATE_AUDIT_TRAIL = "Bundle step has invalid state for adding custom audit trail entry";

		public const int API_ERROR_CODE_LICENSE_ADD_NEW_USER = 210;
		public const string API_MESSAGE_LICENSE_ADD_NEW_USER = "The current license prevents adding new users";

		public const int API_ERROR_CODE_INVALID_ADD_NEW_USER = 211;
		public const string API_MESSAGE_INVALID_ADD_NEW_USER = "Invalid user data.";

	}
}