Imports Winforms

Public Class Frm_Raffles

    Private _AllowEvents As Boolean

    Private Async Sub Frm_Raffles_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadYears()
        Xl_Langs1.Value = Current.Session.Lang
        Await refresca()
        _AllowEvents = True
    End Sub

    Private Async Sub Xl_Raffles1_RefreshToRequest(sender As Object, e As MatEventArgs) Handles Xl_Raffles1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oRaffles = Await FEB2.Raffles.Headers(exs, oLang:=Xl_Langs1.Value, year:=Xl_Years1.Value)
        If exs.Count = 0 Then
            If ComboBoxBrands.Items.Count = 0 Then
                LoadBrands(oRaffles)
            End If
            Xl_Raffles1.Load(oRaffles)
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub LoadYears()
        Dim iYears As New List(Of Integer)
        For i = Today.Year + 1 To 2007 Step -1
            iYears.Add(i)
        Next
        Xl_Years1.Load(iYears, Today.Year)
    End Sub


    Private Sub LoadBrands(oRaffles As List(Of DTORaffle))

        Dim oBrands As List(Of DTOProductBrand) = oRaffles.Where(Function(b) b.Brand IsNot Nothing).GroupBy(Function(x) x.Brand.Guid).Select(Function(y) y.First().Brand).ToList()
        Dim oNoBrand As New DTOProductBrand(System.Guid.Empty)
        oNoBrand.Nom.Esp = Current.Session.Tradueix("(selecciona una marca)", "(selecciona una marca)", "(pick a brand name)", "(selecione a marca)")
        oBrands.Insert(0, oNoBrand)
        Dim items = BrandItem.Collection.Factory(oBrands)
        With ComboBoxBrands
            .DataSource = items
            .DisplayMember = "Nom"
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub ToolStripButtonExcel_Click(sender As Object, e As EventArgs) Handles ToolStripButtonExcel.Click
        Dim oSheet As MatHelperStd.ExcelHelper.Sheet = FEB2.Raffles.Excel(Xl_Raffles1.Values)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ComboBoxBrands_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxBrands.SelectedIndexChanged
        If _AllowEvents Then
            Dim oBrand As DTOProductBrand = ComboBoxBrands.SelectedItem.Brand
            If oBrand.Guid.Equals(Guid.Empty) Then
                Xl_Raffles1.ClearFilter()
            Else
                Xl_Raffles1.Filter = oBrand
            End If
        End If
    End Sub

    Private Async Sub Xl_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate, Xl_Langs1.AfterUpdate
        If _AllowEvents Then
            Await refresca()
        End If
    End Sub

    Private Class BrandItem
        Property Brand As DTOProductBrand
        Property Nom As String

        Public Class Collection
            Inherits List(Of BrandItem)

            Shared Function Factory(oBrands As List(Of DTOProductBrand))
                Dim retval As New Collection
                For Each oBrand In oBrands
                    Dim item As New BrandItem
                    item.Brand = oBrand
                    item.Nom = oBrand.Nom.Tradueix(Current.Session.Lang)
                    retval.Add(item)
                Next
                Return retval
            End Function
        End Class
    End Class
End Class