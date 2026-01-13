Public Class Frm_BancPoolExtracte
    Private _Bank As DTOBank

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oBank As DTOBank)
        MyBase.New
        InitializeComponent()
        _Bank = oBank
    End Sub

    Private Sub Frm_BancPoolExtracte_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEBL.Bank.Load(_Bank, exs) Then
            Me.Text = "Pool bancari " & _Bank.RaoSocial
            refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_BancPoolExtracte1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_BancPoolExtracte1.RequestToRefresh
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub Xl_BancPoolExtracte1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_BancPoolExtracte1.RequestToAddNew
        Dim value As New DTOBancPool
        With value
            .Bank = _Bank
            .Fch = Today
            .ProductCategory = DTOBancPool.ProductCategories.Credit_Comercial
            .Amt = DTOAmt.Empty
        End With
        Dim oFrm As New Frm_BancPool(value)
        AddHandler oFrm.AfterUpdate, AddressOf Refresca
        oFrm.Show()
    End Sub

    Private Sub refresca()
        Dim values As List(Of DTOBancPool) = BLLBancPools.All(_Bank)
        Xl_BancPoolExtracte1.Load(values)
    End Sub
End Class