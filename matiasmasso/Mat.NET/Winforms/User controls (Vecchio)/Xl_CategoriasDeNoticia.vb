Public Class Xl_CategoriasDeNoticia
    Private _Categorias As List(Of DTOCategoriaDeNoticia)
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Async Function Load(oCategorias As List(Of DTOCategoriaDeNoticia)) As Task
        _Categorias = oCategorias
        CheckedListBox1.DisplayMember = "Nom"
        Await RefreshRequest()
        _AllowEvents = True
    End Function

    Public Function CheckedItems() As List(Of DTOCategoriaDeNoticia)
        Dim retval As New List(Of DTOCategoriaDeNoticia)
        For Each itemChecked In CheckedListBox1.CheckedItems
            Dim item As DTOCategoriaDeNoticia = itemChecked
            retval.Add(item)
        Next
        Return retval
    End Function

    Private Function CurrentItem() As DTOCategoriaDeNoticia
        Dim retval As DTOCategoriaDeNoticia = CheckedListBox1.SelectedItem
        Return retval
    End Function

    Private Async Sub RefreshRequest(sender As Object, e As MatEventArgs)
        Await RefreshRequest()
    End Sub

    Private Async Function RefreshRequest() As Task
        CheckedListBox1.Items.Clear()
        Dim exs As New List(Of Exception)
        Dim AllCategorias = Await FEB2.CategoriasDeNoticia.All(exs)
        If exs.Count = 0 Then
            For Each Itm As DTOCategoriaDeNoticia In AllCategorias
                Dim IsChecked As Boolean = False
                If _Categorias IsNot Nothing Then
                    IsChecked = _Categorias.Exists(Function(x) x.Guid.Equals(Itm.Guid))
                End If
                CheckedListBox1.Items.Add(Itm, IsChecked)
            Next
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub CheckedListBox1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(CurrentItem))
        End If
    End Sub

    Private Sub CheckedListBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles CheckedListBox1.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then

            Dim oContextMenu As New ContextMenuStrip
            Dim oCategoria As DTOCategoriaDeNoticia = CurrentItem()

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
        Dim oCategoria As New DTOCategoriaDeNoticia()
        oCategoria.Nom = "(nom de la categoria)"
        Dim oFrm As New Frm_CategoriaDeNoticia(oCategoria)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub
End Class
