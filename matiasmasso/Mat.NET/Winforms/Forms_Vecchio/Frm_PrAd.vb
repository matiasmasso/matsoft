

Public Class Frm_PrAd
    Private _PrAd As PrAd
    Private _AllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Guid
        height
        width
        ImageExists
        Ico
        size
    End Enum

    Public Sub New(ByVal oPrAd As PrAd)
        MyBase.New()
        Me.InitializeComponent()
        PrAdLoader.Load(oPrAd)
        _PrAd = oPrAd
        With _PrAd
            If .Product IsNot Nothing Then
                Xl_Product1.Product_Old = .Product
                TextBoxNom.Text = .Nom
                PictureBoxLogo.Image = .Product.Tpa.Image
                CheckBoxObsoleto.Checked = .Obsoleto
                Xl_PrAdDocs1.Load(oPrAd.Docs)
            End If
        End With
    End Sub



    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxNom.TextChanged, _
     CheckBoxObsoleto.CheckedChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _PrAd
            .Product = Xl_Product1.Product_Old
            .Nom = TextBoxNom.Text
            .obsoleto = CheckBoxObsoleto.Checked
        End With

        Dim exs as New List(Of exception)
        If PrAdLoader.Update(_PrAd, exs) Then
            Dim oArgs As New MatEventArgs(_PrAd)
            RaiseEvent AfterUpdate(Me, oArgs)
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al desar el document")
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub Xl_PrAdDocs1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_PrAdDocs1.RequestToAddNew
        Dim oAdDoc As New PrAdDoc(_PrAd)
        Dim oFrm As New Frm_PrAdDoc(oAdDoc)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshAddDocs
        oFrm.Show()
    End Sub

    Private Sub RefreshAddDocs()
        _PrAd.IsLoaded = False
        PrAdLoader.Load(_PrAd)
        Xl_PrAdDocs1.Load(_PrAd.Docs)
    End Sub
End Class