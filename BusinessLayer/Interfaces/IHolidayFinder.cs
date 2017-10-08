using BusinessLayer.BusinessModels;

namespace BusinessLayer
{
	interface IHolidayFinder
	{
		HolidayInfo Holidays(int CountryId);
	}
}
