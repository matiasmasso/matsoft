Imports C1.Win.C1Chart

Public Class Frm_ProductChart
    Private _Product As Product

    Public Sub New(oProduct As Product)
        MyBase.New()
        Me.InitializeComponent()
        _Product = oProduct
    End Sub

    Private Sub Frm_Test_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = _Product.Text
        With C1Chart1
            .Dock = DockStyle.Fill
        End With
        SetChart()
    End Sub

    Private Sub SetChart()
        ' DataBinding is only available with C1Chart
        ' version 1.0.20034.13244 and later.

        ' get chart data

        Dim SQL As String = GetSQL(_Product)
        Dim conn As String = "Provider=SQLOLEDB;" & MatSQL.GetConnectionString(Databases.Maxi)
        Dim da As System.Data.OleDb.OleDbDataAdapter
        Try
            da = New System.Data.OleDb.OleDbDataAdapter(SQL, conn)
            Dim dt As New DataTable
            da.Fill(dt)

            ' bind chart to table (each series will bind to a field on the table)_
            C1Chart1.DataSource = dt

            ' clear data series collection
            Dim dsc As ChartDataSeriesCollection = C1Chart1.ChartGroups(0).ChartData.SeriesList
            dsc.Clear()

            ' add unit price series
            Dim ds As ChartDataSeries = dsc.AddNewSeries()

            'ds.AutoEnumerate = true' (in case you don't want to set the X values)
            ds.X.DataField = "WEEK"
            ds.Y.DataField = "QTY"


            ds.FitType = FitTypeEnum.Spline
            'ds.Label = "my data label"
            'ds.Display = SeriesDisplayEnum.ExcludeHoles

            Dim oLineStyle As New ChartLineStyle
            oLineStyle.Color = Color.Black
            oLineStyle.Thickness = 2
            ds.LineStyle = oLineStyle


            ' add units in stock series
            'ds = dsc.AddNewSeries()
            'ds.X.DataField = "WEEK"
            'ds.Y.DataField = "QTY"

            ' apply filters, sorting, etc
            'dt.DefaultView.RowFilter = "CategoryID = 4"
        Catch ex As Exception
            Stop
        End Try
    End Sub

    Private Function GetSQL(oProduct As Product) As String

        Dim retval As String = ""
        retval = "SELECT DATEPART(wk,PDC.FCH) AS WEEK, SUM(QTY) AS QTY " _
        & "FROM PNC INNER JOIN " _
        & "PDC ON PNC.PDCGUID=PDC.GUID INNER JOIN " _
        & "ART ON PNC.ARTGUID=ART.GUID " _
        & "WHERE PNC.COD = 2 AND PDC.YEA=" & CurrentYea() & " "

        Select Case oProduct.ValueType
            Case Product.ValueTypes.Art
                retval += "AND ART.Guid='" & oProduct.Guid.ToString & "' "
            Case Product.ValueTypes.Stp
                retval += "AND ART.Category='" & oProduct.Guid.ToString & "' "
        End Select

        retval += "GROUP BY DATEPART(wk,PDC.FCH) " _
        & "ORDER BY WEEK"
        Return retval
    End Function

    Private Function CurrentYea() As Integer
        Return Year(Today)
    End Function
End Class