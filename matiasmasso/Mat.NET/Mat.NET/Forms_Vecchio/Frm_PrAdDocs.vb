Public Class Frm_PrAdDocs
    Private _AllowEvents As Boolean = False
    Private _Revista As PrRevista = Nothing
    Private _SelectionMode As bll.dEFAULTS.SelectionModes

    Public Event AfterSelect(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(oSelectionMode As bll.dEFAULTS.SelectionModes)
        MyBase.New()
        Me.InitializeComponent()
        _SelectionMode = oSelectionMode
    End Sub

    Private Sub Frm_PrAdDocs_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadPrAds()
        LoadPrAdDocs()
    End Sub

    Private Sub LoadPrAds()
        Dim oEmp as DTOEmp = BLL.BLLApp.Emp
        Dim oPrAds As PrAds = PrAdLoader.All(oEmp)
        Xl_PrAds1.Load(oPrAds)
    End Sub

    Private Sub LoadPrAdDocs()
        Dim oPrAd As PrAd = Xl_PrAds1.Value
        Xl_PrAdDocs1.Load(oPrAd.Docs)
    End Sub

    Private Sub Xl_PrAds1_AfterSelect(sender As Object, e As MatEventArgs) Handles Xl_PrAds1.AfterSelect
        Dim oPrAd As PrAd = e.Argument
        Xl_PrAdDocs1.Load(oPrAd.Docs)
    End Sub

    Private Sub Xl_PrAdDocs1_AfterSelect(sender As Object, e As MatEventArgs) Handles Xl_PrAdDocs1.AfterSelect
        Select Case _SelectionMode
            Case bll.dEFAULTS.SelectionModes.Selection
                RaiseEvent AfterSelect(Me, e)
                Me.Close()
            Case Else
                Dim oPrAdDoc As PrAdDoc = e.Argument
                Dim oFrm As New Frm_PrAdDoc(oPrAdDoc)
                AddHandler oFrm.AfterUpdate, AddressOf LoadPrAdDocs
                oFrm.Show()
        End Select
    End Sub
End Class