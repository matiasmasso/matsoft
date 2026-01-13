

Public Class Frm_PaymentTerms
    Public Event AfterUpdate(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Private _PaymentTerms As DTOPaymentTerms

    Public Sub New(ByVal oPaymentTerms As DTOPaymentTerms)
        MyBase.New()
        Me.InitializeComponent()
        _PaymentTerms = oPaymentTerms

        Refresca()
    End Sub

    Private Sub Refresca()
        LoadModes()
        LoadPlazos()
        ButtonDel.Enabled = True
    End Sub

    Private Sub LoadModes()
        Dim v As Integer
        For Each v In [Enum].GetValues(GetType(DTOPaymentTerms.CodsFormaDePago))
            Dim oListItem As New ListItem
            oListItem.Value = v
            oListItem.Text = DTOPaymentTerms.CfpText(v, Current.Session.Lang)
            ComboBoxModes.Items.Add(oListItem)

            If _PaymentTerms IsNot Nothing AndAlso _PaymentTerms.Cod = v Then
                ComboBoxModes.SelectedItem = oListItem
            End If
        Next
        If ComboBoxModes.SelectedIndex = -1 Then ComboBoxModes.SelectedIndex = 0
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If _PaymentTerms Is Nothing Then _PaymentTerms = New DTOPaymentTerms
        With _PaymentTerms
            .Cod = DirectCast(ComboBoxModes.SelectedItem, ListItem).Value
            .Plazos = GetPlazosFromCombobox()
        End With
        If _PaymentTerms.Cod = DTOPaymentTerms.CodsFormaDePago.NotSet Then _PaymentTerms = Nothing
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_PaymentTerms))
        Me.Close()
    End Sub

    Public Sub LoadPlazos()
        Dim oPlazos As New List(Of DTOPaymentTerms.Plazo)
        For Each period In [Enum].GetValues(GetType(DTOPaymentTerms.Plazo.Periods))
            Dim oPlazo As New DTOPaymentTerms.Plazo(period)
            If _PaymentTerms IsNot Nothing AndAlso _PaymentTerms.Plazos.Any(Function(x) x.Period = oPlazo.Period) Then
                ListBox2.Items.Add(oPlazo)
            Else
                ListBox1.Items.Add(oPlazo)
            End If
        Next
        ListBox1.DisplayMember = "period"
        ListBox2.DisplayMember = "period"
        EnableButtons()
    End Sub


    Private Sub ButtonAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAdd.Click
        Dim oItem As Object = ListBox1.SelectedItem
        ListBox2.Items.Add(oItem)
        ListBox1.Items.Remove(oItem)
        EnableButtons()
    End Sub

    Private Sub ButtonRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonRemove.Click
        Dim oItem As Object = ListBox2.SelectedItem
        ListBox1.Items.Add(oItem)
        ListBox2.Items.Remove(oItem)
        EnableButtons()
    End Sub

    Private Sub EnableButtons()
        ButtonAdd.Enabled = (ListBox1.SelectedIndex >= 0)
        ButtonRemove.Enabled = (ListBox1.SelectedIndex >= 0)
        ButtonOk.Enabled = (ComboBoxModes.SelectedItem IsNot Nothing)
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        EnableButtons()
    End Sub

    Private Function GetPlazosFromCombobox()
        Dim retval As New List(Of DTOPaymentTerms.Plazo)
        For Each oItem As DTOPaymentTerms.Plazo In ListBox2.Items
            retval.Add(oItem)
        Next
        Return retval
    End Function


    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        _PaymentTerms = Nothing
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_PaymentTerms))
        Me.Close()
    End Sub

    Private Sub ComboBoxModes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxModes.SelectedIndexChanged
        EnableButtons()
    End Sub
End Class