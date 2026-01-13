Public Class Frm_PrAdDoc
    Private _PrAdDoc As PrAdDoc
    Private _AllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oAdDoc As PrAdDoc)
        MyBase.New()
        Me.InitializeComponent()
        PrAdDocLoader.Load(oAdDoc)
        _PrAdDoc = oAdDoc
        With _PrAdDoc
            If .Ad IsNot Nothing Then
                TextBoxAdNom.Text = .Ad.Nom
                Xl_DocFile1.Load(.DocFile)
                If .Ad.Product IsNot Nothing Then
                    PictureBoxLogo.Image = .Ad.Product.Tpa.Image
                End If
            End If
            NumericUpDownWidth.Value = .Size.Width
            NumericUpDownHeight.Value = .Size.Height
        End With
        _AllowEvents = True
    End Sub


    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    NumericUpDownWidth.ValueChanged, _
    NumericUpDownHeight.ValueChanged, _
     Xl_DocFile1.AfterFileDropped

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _PrAdDoc
            .Size = New Size(NumericUpDownWidth.Value, NumericUpDownHeight.Value)
            If Xl_DocFile1.IsDirty Then
                .DocFile = Xl_DocFile1.Value
            End If
        End With

        Dim exs as New List(Of exception)
        If PrAdDocLoader.Update(_PrAdDoc, exs) Then
            Dim oArgs As New MatEventArgs(_PrAdDoc)
            RaiseEvent AfterUpdate(Me, oArgs)
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al desar la creativitat")
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


End Class