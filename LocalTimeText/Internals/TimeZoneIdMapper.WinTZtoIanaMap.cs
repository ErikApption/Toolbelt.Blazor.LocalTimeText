namespace Toolbelt.Blazor.Globalization.Internals;

internal partial class TimeZoneIdMapper
{
    private const string WinTzToIanaMap = "\r\n" +
        "Dateline Standard Time\tEtc/GMT+12\r\n" +
        "UTC-11\tEtc/GMT+11\r\n" +
        "Aleutian Standard Time\tAmerica/Adak\r\n" +
        "Hawaiian Standard Time\tPacific/Honolulu\r\n" +
        "Marquesas Standard Time\tPacific/Marquesas\r\n" +
        "Alaskan Standard Time\tAmerica/Anchorage\r\n" +
        "UTC-09\tEtc/GMT+9\r\n" +
        "Pacific Standard Time (Mexico)\tAmerica/Tijuana\r\n" +
        "UTC-08\tEtc/GMT+8\r\n" +
        "Pacific Standard Time\tAmerica/Los_Angeles\r\n" +
        "US Mountain Standard Time\tAmerica/Phoenix\r\n" +
        "Mountain Standard Time (Mexico)\tAmerica/Chihuahua\r\n" +
        "Mountain Standard Time\tAmerica/Denver\r\n" +
        "Yukon Standard Time\tAmerica/Whitehorse\r\n" +
        "Central America Standard Time\tAmerica/Guatemala\r\n" +
        "Central Standard Time\tAmerica/Chicago\r\n" +
        "Easter Island Standard Time\tPacific/Easter\r\n" +
        "Central Standard Time (Mexico)\tAmerica/Mexico_City\r\n" +
        "Canada Central Standard Time\tAmerica/Regina\r\n" +
        "SA Pacific Standard Time\tAmerica/Bogota\r\n" +
        "Eastern Standard Time (Mexico)\tAmerica/Cancun\r\n" +
        "Eastern Standard Time\tAmerica/New_York\r\n" +
        "Haiti Standard Time\tAmerica/Port-au-Prince\r\n" +
        "Cuba Standard Time\tAmerica/Havana\r\n" +
        "US Eastern Standard Time\tAmerica/Indianapolis\r\n" +
        "Turks And Caicos Standard Time\tAmerica/Grand_Turk\r\n" +
        "Paraguay Standard Time\tAmerica/Asuncion\r\n" +
        "Atlantic Standard Time\tAmerica/Halifax\r\n" +
        "Venezuela Standard Time\tAmerica/Caracas\r\n" +
        "Central Brazilian Standard Time\tAmerica/Cuiaba\r\n" +
        "SA Western Standard Time\tAmerica/La_Paz\r\n" +
        "Pacific SA Standard Time\tAmerica/Santiago\r\n" +
        "Newfoundland Standard Time\tAmerica/St_Johns\r\n" +
        "Tocantins Standard Time\tAmerica/Araguaina\r\n" +
        "E. South America Standard Time\tAmerica/Sao_Paulo\r\n" +
        "SA Eastern Standard Time\tAmerica/Cayenne\r\n" +
        "Argentina Standard Time\tAmerica/Buenos_Aires\r\n" +
        "Montevideo Standard Time\tAmerica/Montevideo\r\n" +
        "Magallanes Standard Time\tAmerica/Punta_Arenas\r\n" +
        "Saint Pierre Standard Time\tAmerica/Miquelon\r\n" +
        "Bahia Standard Time\tAmerica/Bahia\r\n" +
        "UTC-02\tEtc/GMT+2\r\n" +
        "Greenland Standard Time\tAmerica/Godthab\r\n" +
        "Azores Standard Time\tAtlantic/Azores\r\n" +
        "Cape Verde Standard Time\tAtlantic/Cape_Verde\r\n" +
        "UTC\tEtc/UTC\r\n" +
        "GMT Standard Time\tEurope/London\r\n" +
        "Greenwich Standard Time\tAtlantic/Reykjavik\r\n" +
        "Sao Tome Standard Time\tAfrica/Sao_Tome\r\n" +
        "Morocco Standard Time\tAfrica/Casablanca\r\n" +
        "W. Europe Standard Time\tEurope/Berlin\r\n" +
        "Central Europe Standard Time\tEurope/Budapest\r\n" +
        "Romance Standard Time\tEurope/Paris\r\n" +
        "Central European Standard Time\tEurope/Warsaw\r\n" +
        "W. Central Africa Standard Time\tAfrica/Lagos\r\n" +
        "GTB Standard Time\tEurope/Bucharest\r\n" +
        "Middle East Standard Time\tAsia/Beirut\r\n" +
        "Egypt Standard Time\tAfrica/Cairo\r\n" +
        "E. Europe Standard Time\tEurope/Chisinau\r\n" +
        "West Bank Standard Time\tAsia/Hebron\r\n" +
        "South Africa Standard Time\tAfrica/Johannesburg\r\n" +
        "FLE Standard Time\tEurope/Kiev\r\n" +
        "Israel Standard Time\tAsia/Jerusalem\r\n" +
        "South Sudan Standard Time\tAfrica/Juba\r\n" +
        "Kaliningrad Standard Time\tEurope/Kaliningrad\r\n" +
        "Sudan Standard Time\tAfrica/Khartoum\r\n" +
        "Libya Standard Time\tAfrica/Tripoli\r\n" +
        "Namibia Standard Time\tAfrica/Windhoek\r\n" +
        "Jordan Standard Time\tAsia/Amman\r\n" +
        "Arabic Standard Time\tAsia/Baghdad\r\n" +
        "Syria Standard Time\tAsia/Damascus\r\n" +
        "Turkey Standard Time\tEurope/Istanbul\r\n" +
        "Arab Standard Time\tAsia/Riyadh\r\n" +
        "Belarus Standard Time\tEurope/Minsk\r\n" +
        "Russian Standard Time\tEurope/Moscow\r\n" +
        "E. Africa Standard Time\tAfrica/Nairobi\r\n" +
        "Volgograd Standard Time\tEurope/Volgograd\r\n" +
        "Iran Standard Time\tAsia/Tehran\r\n" +
        "Arabian Standard Time\tAsia/Dubai\r\n" +
        "Astrakhan Standard Time\tEurope/Astrakhan\r\n" +
        "Azerbaijan Standard Time\tAsia/Baku\r\n" +
        "Russia Time Zone 3\tEurope/Samara\r\n" +
        "Mauritius Standard Time\tIndian/Mauritius\r\n" +
        "Saratov Standard Time\tEurope/Saratov\r\n" +
        "Georgian Standard Time\tAsia/Tbilisi\r\n" +
        "Caucasus Standard Time\tAsia/Yerevan\r\n" +
        "Afghanistan Standard Time\tAsia/Kabul\r\n" +
        "West Asia Standard Time\tAsia/Tashkent\r\n" +
        "Qyzylorda Standard Time\tAsia/Qyzylorda\r\n" +
        "Ekaterinburg Standard Time\tAsia/Yekaterinburg\r\n" +
        "Pakistan Standard Time\tAsia/Karachi\r\n" +
        "India Standard Time\tAsia/Calcutta\r\n" +
        "Sri Lanka Standard Time\tAsia/Colombo\r\n" +
        "Nepal Standard Time\tAsia/Katmandu\r\n" +
        "Central Asia Standard Time\tAsia/Almaty\r\n" +
        "Bangladesh Standard Time\tAsia/Dhaka\r\n" +
        "Omsk Standard Time\tAsia/Omsk\r\n" +
        "Myanmar Standard Time\tAsia/Rangoon\r\n" +
        "SE Asia Standard Time\tAsia/Bangkok\r\n" +
        "Altai Standard Time\tAsia/Barnaul\r\n" +
        "W. Mongolia Standard Time\tAsia/Hovd\r\n" +
        "North Asia Standard Time\tAsia/Krasnoyarsk\r\n" +
        "N. Central Asia Standard Time\tAsia/Novosibirsk\r\n" +
        "Tomsk Standard Time\tAsia/Tomsk\r\n" +
        "China Standard Time\tAsia/Shanghai\r\n" +
        "North Asia East Standard Time\tAsia/Irkutsk\r\n" +
        "Singapore Standard Time\tAsia/Singapore\r\n" +
        "W. Australia Standard Time\tAustralia/Perth\r\n" +
        "Taipei Standard Time\tAsia/Taipei\r\n" +
        "Ulaanbaatar Standard Time\tAsia/Ulaanbaatar\r\n" +
        "Aus Central W. Standard Time\tAustralia/Eucla\r\n" +
        "Transbaikal Standard Time\tAsia/Chita\r\n" +
        "Tokyo Standard Time\tAsia/Tokyo\r\n" +
        "North Korea Standard Time\tAsia/Pyongyang\r\n" +
        "Korea Standard Time\tAsia/Seoul\r\n" +
        "Yakutsk Standard Time\tAsia/Yakutsk\r\n" +
        "Cen. Australia Standard Time\tAustralia/Adelaide\r\n" +
        "AUS Central Standard Time\tAustralia/Darwin\r\n" +
        "E. Australia Standard Time\tAustralia/Brisbane\r\n" +
        "AUS Eastern Standard Time\tAustralia/Sydney\r\n" +
        "West Pacific Standard Time\tPacific/Port_Moresby\r\n" +
        "Tasmania Standard Time\tAustralia/Hobart\r\n" +
        "Vladivostok Standard Time\tAsia/Vladivostok\r\n" +
        "Lord Howe Standard Time\tAustralia/Lord_Howe\r\n" +
        "Bougainville Standard Time\tPacific/Bougainville\r\n" +
        "Russia Time Zone 10\tAsia/Srednekolymsk\r\n" +
        "Magadan Standard Time\tAsia/Magadan\r\n" +
        "Norfolk Standard Time\tPacific/Norfolk\r\n" +
        "Sakhalin Standard Time\tAsia/Sakhalin\r\n" +
        "Central Pacific Standard Time\tPacific/Guadalcanal\r\n" +
        "Russia Time Zone 11\tAsia/Kamchatka\r\n" +
        "New Zealand Standard Time\tPacific/Auckland\r\n" +
        "UTC+12\tEtc/GMT-12\r\n" +
        "Fiji Standard Time\tPacific/Fiji\r\n" +
        "Chatham Islands Standard Time\tPacific/Chatham\r\n" +
        "UTC+13\tEtc/GMT-13\r\n" +
        "Tonga Standard Time\tPacific/Tongatapu\r\n" +
        "Samoa Standard Time\tPacific/Apia\r\n" +
        "Line Islands Standard Time\tPacific/Kiritimati\r\n" +
        "";
}
