Public Class Frm_ProductPlugin
    Private _ProductPlugin As DTOProductPlugin
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOProductPlugin)
        MyBase.New()
        Me.InitializeComponent()
        _ProductPlugin = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub refresca()
        Dim exs As New List(Of Exception)
        If FEB.ProductPlugin.Load(exs, _ProductPlugin) Then
            With _ProductPlugin
                TextBoxNom.Text = .Nom
                Dim oProducts As New List(Of DTOProduct)
                If .Product IsNot Nothing Then oProducts.Add(.Product)
                Xl_LookupProduct1.Load(oProducts, DTOProduct.SelectionModes.SelectAny)
                Xl_ProductPluginItems1.Load(_ProductPlugin)
                Xl_UsrLog1.Load(.UsrLog)
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
         Xl_ProductPluginItems1.AfterUpdate

        If _AllowEvents Then
            _ProductPlugin.Items = Xl_ProductPluginItems1.Values
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        LoadFromForm()
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.ProductPlugin.Update(exs, _ProductPlugin) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductPlugin))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub LoadFromForm()
        With _ProductPlugin
            .nom = TextBoxNom.Text
            .product = Xl_LookupProduct1.Product
            .items = Xl_ProductPluginItems1.Values
        End With
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.ProductPlugin.Delete(exs, _ProductPlugin) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductPlugin))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Async Sub AfegirProducteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AfegirProducteToolStripMenuItem.Click
        Await AddItem()
    End Sub

    Private Sub CopyCodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyCodeToolStripMenuItem.Click
        Dim src As String = _ProductPlugin.Snippet
        Clipboard.SetDataObject(src, True)
        MsgBox("Plugin copiat al portapapers", MsgBoxStyle.Information, "Mat.Net")
    End Sub

    Private Async Function AddItem() As Task
        Dim exs As New List(Of Exception)
        Dim item = DTOProductPlugin.Item.Factory(_ProductPlugin)
        If _ProductPlugin.IsNew Then
            LoadFromForm()
            If Not Await FEB.ProductPlugin.Update(exs, _ProductPlugin) Then
                UIHelper.WarnError(exs)
                Exit Function
            End If
        End If
        Dim oFrm As New Frm_ProductPluginItem(item)
        AddHandler oFrm.AfterUpdate, AddressOf onItemAdded
        oFrm.Show()
    End Function

    Private Sub onItemAdded(sender As Object, e As MatEventArgs)
        _ProductPlugin.items.Add(e.Argument)
        Xl_ProductPluginItems1.Load(_ProductPlugin)
        ButtonOk.Enabled = True
    End Sub

    Private Async Sub Xl_ProductPluginItems1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductPluginItems1.RequestToAddNew
        Await AddItem()
    End Sub

    Private Sub Xl_ProductPluginItems1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductPluginItems1.RequestToRefresh
        ButtonOk.Enabled = True
    End Sub
End Class