Public Class Frm_Extracte

    Private _Contact As DTOContact
    Private _Cta As DTOPgcCta

    Private Enum Tabs
        General
        Pnds
    End Enum

    Public Sub New(oContact As DTOContact, Optional oCta As DTOPgcCta = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        _Contact = oContact
        _Cta = oCta
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        BLL.BLLContact.Load(_Contact)
        Me.Text = "Comptes de " & _Contact.FullNom

        LoadYears()
        LoadCtas(DefaultCta)
        LoadCcbs()
    End Sub

    Private Function DefaultCta() As DTOPgcCta
        Dim retval As DTOPgcCta = _Cta
        If retval Is Nothing Then
            Dim oExercici As DTOExercici = BLL.BLLExercici.FromYear(CurrentYear)
            Select Case _Contact.Rol.Id
                Case DTORol.Ids.Banc
                    retval = BLL.BLLPgcCta.FromCod(DTOPgcPlan.Ctas.bancs, oExercici)
                Case DTORol.Ids.Cli
                    retval = BLL.BLLPgcCta.FromCod(DTOPgcPlan.Ctas.Clients, oExercici)
                Case DTORol.Ids.Manufacturer
                    retval = BLL.BLLPgcCta.FromCod(DTOPgcPlan.Ctas.proveidorsEur, oExercici)
            End Select
        End If
        Return retval
    End Function

    Private Function CurrentContact() As DTOContact
        Return _Contact
    End Function

    Private Function CurrentCta() As DTOPgcCta
        Dim retval As DTOPgcCta = Xl_Extracte_Ctas1.Value
        Return retval
    End Function

    Private Function CurrentYear() As Integer
        Dim retval As Integer = Xl_Extracte_Years1.Value
        Return retval
    End Function

    Private Sub LoadYears()
        Dim iYears As List(Of Integer) = BLL.BLLExtracte.Years(CurrentContact)
        Xl_Extracte_Years1.Load(iYears)
    End Sub

    Private Sub LoadCtas(Optional oDefaultCta As DTOPgcCta = Nothing)
        Dim oCtas As List(Of DTOPgcCta) = BLL.BLLExtracte.Ctas(CurrentYear, CurrentContact)
        Xl_Extracte_Ctas1.Load(oCtas, oDefaultCta)
    End Sub

    Private Sub LoadCcbs()
        Dim oExtracte As New DTOExtracte
        With oExtracte
            .Exercici = BLL.BLLExercici.FromYear(CurrentYear)
            .Cta = CurrentCta()
            .Contact = CurrentContact()
            .Ccbs = BLL.BLLExtracte.Ccbs(oExtracte)
        End With
        Xl_Extracte_Ccbs1.Load(oExtracte)
    End Sub

    Private Sub Xl_Extracte_Years1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Extracte_Years1.onItemSelected
        LoadCtas()
        LoadCcbs()
    End Sub

    Private Sub Xl_Extracte_Ctas1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Extracte_Ctas1.onItemSelected
        LoadCcbs()
    End Sub

    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        Dim sSearchKey As String = TextBoxSearch.Text
        If sSearchKey.Length > 1 Then
            TextBoxSearch.ForeColor = Color.Black
            Xl_Extracte_Ccbs1.Filter = sSearchKey
            Xl_Pnds1.Filter = sSearchKey
        Else
            Xl_Extracte_Ccbs1.ClearFilter()
            Xl_Pnds1.ClearFilter()
            TextBoxSearch.ForeColor = Color.Gray
        End If

    End Sub

    Private Sub Xl_Extracte_Ccbs1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Extracte_Ccbs1.RequestToAddNew
        Dim oCca As DTOCca=BLL.BLLCca.NewCca(Today, BLL.BLLSession.Current.User, DTOCca.CcdEnum.Manual)
        BLL.BLLCca.AddCredit(oCca, New DTOAmt, CurrentCta, CurrentContact)
        Dim oFrm As New Frm_Cca(oCca)
        AddHandler oFrm.AfterUpdate, AddressOf LoadCcbs
        oFrm.Show()
    End Sub

    Private Function SumPnds(oPnds As List(Of DTOPnd)) As DTOAmt
        Dim retval As DTOAmt
        Dim DcDeb As Decimal = oPnds.Where(Function(x) x.Cod = DTOPnd.Codis.Deutor).Sum(Function(x) x.Amt.Eur)
        Dim DcHab As Decimal = oPnds.Where(Function(x) x.Cod = DTOPnd.Codis.Creditor).Sum(Function(x) x.Amt.Eur)
        Select Case _Contact.Rol.Id
            Case DTORol.Ids.Manufacturer
                retval = New DTOAmt(DcHab - DcDeb)
            Case Else
                retval = New DTOAmt(DcDeb - DcHab)
        End Select
        Return retval
    End Function

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Pnds
                Static TabPndsLoaded As Boolean
                If Not TabPndsLoaded Then
                    RefrescaPnds()
                    TabPndsLoaded = True
                End If
        End Select
    End Sub

    Private Sub Xl_Pnds1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Pnds1.RequestToAddNew
        Dim oPnd As New Pnd()
        With oPnd
            .Contact = New Contact(_Contact.Guid)
            .Fch = Today
            .Vto = Today
            .Amt = New MaxiSrvr.Amt(0, MaxiSrvr.Cur.Eur, 0)
        End With

        Dim oFrm As New Frm_Contact_Pnd(oPnd)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaPnds
        oFrm.Show()
    End Sub

    Private Sub Xl_Pnds1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Pnds1.RequestToRefresh
        RefrescaPnds()
    End Sub

    Private Sub RefrescaPnds()
        Dim oPnds As List(Of DTOPnd) = BLL.BLLPnds.All(_Contact)
        LabelSumPnds.Text = "Total pendent de liquidar: " & SumPnds(oPnds).CurFormatted
        Xl_Pnds1.Load(oPnds)
    End Sub

    Private Sub Xl_Extracte_Ccbs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Extracte_Ccbs1.RequestToRefresh
        LoadCcbs()
    End Sub
End Class