@Code
    Layout = "~/Views/Shared/_Layout_FullWidth.vbhtml"

    Dim oUser = Mvc.ContextHelper.FindUserSync()
    Dim oProveidor As DTOProveidor = DTOProveidor.Wellknown(DTOProveidor.Wellknowns.Mayborn)

End Code


<div class="pagewrapper">

    <div style="text-align:center;">

        <section class="PageTitle">
            Mayborn
        </section>

        <div>
            <a href='@FEB2.Mayborn.CsvUrl()' title='Download Csv file'>
                monthly sales data summary (for Malk)
            </a>
        </div>

        <div>
            <a href='@FEB2.SellOut.RawDataLast12MonthsCsvUrl()' title='Download Sellout Csv file'>
                product sold x month and customer (for Guillermo)
            </a>
        </div>
        <div>
            <a href='@FEB2.ProductDistributors.PerChannelUrl(Today.Year)' title='Download Sellout per channel Excel file'>
                channel split sell out
            </a>
        </div>
    </div>

    </div>

