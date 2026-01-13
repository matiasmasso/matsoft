Public Class Frm_Ranking
    Private _Ranking As Ranking
    Private _Allowevents As Boolean

    Public Sub New(oRanking As Ranking)
        MyBase.New()
        Me.InitializeComponent()
        _Ranking = oRanking
    End Sub

    Private Sub Frm_Ranking_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Ranking
            DateTimePickerTo.Value = .FchTo
            DateTimePickerFrom.Value = .FchFrom
            Xl_ContactsComboProveidors.Load(.Proveidors, .Proveidor, .User.Lang.Tradueix("(todos los proveedores)", "(tots els proveïdors)", "(any supplier)"))
            Xl_RepsCombo1.Load(.Reps, .Rep)
            Xl_Lookup_ProductBase1.Load(.Catalog, .Product)
            Xl_Lookup_DTOContact1.Load(.Atlas, .Area)
            Xl_RankingItems1.Load(_Ranking)
        End With
        _Allowevents = True
    End Sub

    Private Sub Xl_ContactsComboProveidors_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_ContactsComboProveidors.AfterUpdate
        If _Allowevents Then
            Dim oProveidor As Contact = e.Argument
            BLL_Ranking.SetProveidor(_Ranking, oProveidor)
            Xl_Lookup_ProductBase1.Load(_Ranking.Catalog, _Ranking.Product)
            Xl_RankingItems1.Load(_Ranking)
        End If
    End Sub

    Private Sub Xl_Lookup_ProductBase1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Lookup_ProductBase1.AfterUpdate
        If _Allowevents Then
            Dim oProduct As DTOProduct0 = e.Argument
            BLL_Ranking.SetProduct(_Ranking, oProduct)
            Xl_RankingItems1.Load(_Ranking)
        End If
    End Sub

    Private Sub Xl_RepsCombo1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_RepsCombo1.AfterUpdate
        If _Allowevents Then
            Dim oRep As DTORep = e.Argument
            BLL_Ranking.SetRep(_Ranking, oRep)
            Xl_RankingItems1.Load(_Ranking)
        End If
    End Sub

    Private Sub Xl_Lookup_DTOContact1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Lookup_DTOContact1.AfterUpdate
        If _Allowevents Then
            Dim oArea As DTOAreaOld = e.Argument
            BLL_Ranking.SetArea(_Ranking, oArea)
            Xl_RankingItems1.Load(_Ranking)
        End If
    End Sub

    Private Sub DateTimePickerTo_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePickerTo.ValueChanged
        If _Allowevents Then
            If DateTimePickerFrom.Value > DateTimePickerTo.Value Then
                DateTimePickerFrom.Value = DateTimePickerTo.Value
            End If
            DateTimePickerFrom.MaxDate = DateTimePickerTo.Value
            BLL_Ranking.SetFchs(_Ranking, DateTimePickerFrom.Value, DateTimePickerTo.Value)
            Xl_RankingItems1.Load(_Ranking)
        End If
    End Sub

    Private Sub DateTimePickerFrom_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePickerFrom.ValueChanged
        If _Allowevents Then
            BLL_Ranking.SetFchs(_Ranking, DateTimePickerFrom.Value, DateTimePickerTo.Value)
            Xl_Lookup_ProductBase1.Load(_Ranking.Catalog, _Ranking.Product)
            Xl_RankingItems1.Load(_Ranking)
        End If
    End Sub

    Private Sub PictureBoxExcel_Click(sender As Object, e As EventArgs) Handles PictureBoxExcel.Click
        Dim oExcelSheet As DTOExcelSheet = BLL_Ranking.ExcelSheet(_Ranking)
        UIHelper.ShowExcel(oExcelSheet)
    End Sub
End Class