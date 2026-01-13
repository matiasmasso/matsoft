Public Class Xl_CategoriasDeNoticia
    Private _Categorias As CategoriasDeNoticia
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(oCategorias As CategoriasDeNoticia)
        _Categorias = oCategorias
        CheckedListBox1.DisplayMember = "Nom"
        RefreshRequest()
        _AllowEvents = True
    End Sub

    Public Function CheckedItems() As CategoriasDeNoticia
        Dim retval As New CategoriasDeNoticia
        For Each itemChecked In CheckedListBox1.CheckedItems
            Dim item As CategoriaDeNoticia = itemChecked
            retval.Add(item)
        Next
        Return retval
    End Function

    Private Function CurrentItem() As CategoriaDeNoticia
        Dim retval As CategoriaDeNoticia = CheckedListBox1.SelectedItem
        Return retval
    End Function

    Private Sub RefreshRequest()
        CheckedListBox1.Items.Clear()
        Dim AllCategorias As CategoriasDeNoticia = CategoriasDeNoticiaLoader.All
        For Each Itm As CategoriaDeNoticia In AllCategorias
            Dim IsChecked As Boolean = False
            If _Categorias IsNot Nothing Then
                IsChecked = _Categorias.Exists(Function(x) x.Guid.Equals(Itm.Guid))
            End If
            CheckedListBox1.Items.Add(Itm, IsChecked)
        Next
    End Sub

    Private Sub CheckedListBox1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(CurrentItem))
        End If
    End Sub

    Private Sub CheckedListBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles CheckedListBox1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim oContextMenu As New ContextMenuStrip
            Dim oCategoria As CategoriaDeNoticia = CurrentItem()

            If oCategoria IsNot Nothing Then
                Dim oMenu_Categoria As New Menu_CategoriaDeNoticia(CurrentItem)
                AddHandler oMenu_Categoria.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu_Categoria.Range)
                oContextMenu.Items.Add("-")
            End If
            oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

            CheckedListBox1.ContextMenuStrip = oContextMenu

        End If
    End Sub

    Private Sub Do_AddNew()
        Dim oCategoria As New CategoriaDeNoticia("(nom de la categoria)")
        Dim oFrm As New Frm_CategoriaDeNoticia(oCategoria)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub
End Class
