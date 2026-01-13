Public Class Frm_ProductChannel

    Private _ProductChannel As DTOProductChannel
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOProductChannel)
        MyBase.New()
        Me.InitializeComponent()
        _ProductChannel = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.ProductChannel.Load(_ProductChannel, exs) Then
            With _ProductChannel
                Dim oProducts As New List(Of DTOProduct)
                If .Product IsNot Nothing Then oProducts.Add(.Product)
                Xl_LookupProduct1.Load(oProducts, DTOProduct.SelectionModes.SelectAny)
                Xl_LookupDistributionChannel1.DistributionChannel = .DistributionChannel
                If .Cod = DTOProductChannel.Cods.Exclou Then
                    RadioButtonExclos.Checked = True
                End If
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        Xl_LookupProduct1.AfterUpdate,
         Xl_LookupDistributionChannel1.AfterUpdate,
          RadioButtonInclos.CheckedChanged,
           RadioButtonExclos.CheckedChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _ProductChannel
            .Product = Xl_LookupProduct1.Product
            .DistributionChannel = Xl_LookupDistributionChannel1.DistributionChannel
            .Cod = IIf(RadioButtonInclos.Checked, DTOProductChannel.Cods.Inclou, DTOProductChannel.Cods.Exclou)
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.ProductChannel.Update(_ProductChannel, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductChannel))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.ProductChannel.Delete(_ProductChannel, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductChannel))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


