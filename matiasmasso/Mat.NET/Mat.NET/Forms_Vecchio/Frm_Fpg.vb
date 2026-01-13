

Public Class Frm_Fpg
    Public Event AfterUpdate(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Private mFpg As fpg

    Public Sub New(ByVal oFpg As fpg)
        MyBase.New()
        Me.InitializeComponent()
        mFpg = oFpg
        Refresca()
    End Sub

    Private Sub Refresca()
        LoadModes()
        ComboBoxModes.SelectedIndex = mFpg.Modalitat
        LoadPlazos()
        ButtonDel.Enabled = (mFpg.Modalitat <> MaxiSrvr.fpg.Modalitats.NotSet)
    End Sub

    Private Sub LoadModes()
        Dim v As Integer
        For Each v In [Enum].GetValues(GetType(MaxiSrvr.fpg.Modalitats))
            Dim oListItem As New maxisrvr.ListItem
            oListItem.Value = v
            oListItem.Text = MaxiSrvr.fpg.ModalidadText(v)
            ComboBoxModes.Items.Add(oListItem)
        Next
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mFpg
            .Modalitat = ComboBoxModes.SelectedIndex
            .Plazos = GetPlazosFromCombobox
        End With
        RaiseEvent AfterUpdate(mFpg, New System.EventArgs)
        Me.Close()
    End Sub

    Public Sub LoadPlazos()
        Dim v As Integer
        Dim oListBox As ListBox
        For Each v In [Enum].GetValues(GetType(MaxiSrvr.fpg.Plazo))
            oListBox = ListBox1
            Dim oListItem As New maxisrvr.ListItem
            oListItem.Value = v
            oListItem.Text = MaxiSrvr.fpg.PlazoText(v)
            For Each oPlazo As fpg.Plazo In mFpg.Plazos
                If oPlazo = v Then
                    oListBox = ListBox2
                    Exit For
                End If
            Next
            oListBox.Items.Add(oListItem)
        Next

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
        ButtonOk.Enabled = (ComboBoxModes.SelectedItem.value <> MaxiSrvr.fpg.Modalitats.NotSet)
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        EnableButtons()
    End Sub

    Private Function GetPlazosFromCombobox()
        Dim oArray As New ArrayList
        For Each oItem As maxisrvr.ListItem In ListBox2.Items
            oArray.Add(CType(oItem.Value, fpg.Plazo))
        Next
        Return oArray
    End Function


    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        mFpg.Clear()
        RaiseEvent AfterUpdate(mFpg, New System.EventArgs)
        Me.Close()
    End Sub

    Private Sub ComboBoxModes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxModes.SelectedIndexChanged
        EnableButtons()
    End Sub
End Class