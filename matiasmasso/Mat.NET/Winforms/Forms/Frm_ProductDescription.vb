Public Class Frm_ProductDescription
    Private _Product As DTOProduct
    Private _DirtyUrls As Boolean
    Private _AllowEvents As Boolean
    Dim maxLengthExtracte = 156

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oProduct As DTOProduct)
        MyBase.New
        InitializeComponent()

        _Product = oProduct
    End Sub

    Private Sub Frm_ProductDescription_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _Product.IsNew Then
            Me.Text = "Noms i Descripcions de nou producte"
        Else
            Me.Text = "Noms i Descripcions de producte: " & _Product.FullNom()
            FEB2.Product.Load(_Product, exs)
        End If

        If exs.Count = 0 Then
            With _Product
                With .Nom
                    TextBoxNomEsp.Text = .Esp
                    TextBoxNomCat.Text = .Cat
                    TextBoxNomEng.Text = .Eng
                    TextBoxNomPor.Text = .Por
                End With

                TextBoxUrlEsp.Text = .CanonicalUrlSegment(DTOLang.ESP())
                TextBoxUrlCat.Text = .CanonicalUrlSegment(DTOLang.CAT())
                TextBoxUrlEng.Text = .CanonicalUrlSegment(DTOLang.ENG())
                TextBoxUrlPor.Text = .CanonicalUrlSegment(DTOLang.POR())


                TextBoxExcerptEsp.Text = .Excerpt.Esp
                TextBoxExcerptCat.Text = .Excerpt.Cat
                TextBoxExcerptEng.Text = .Excerpt.Eng
                TextBoxExcerptPor.Text = .Excerpt.Por

                Xl_LangsTextDsc.Load(.Content)

                For Each segment In .UrlSegments.Where(Function(x) x.Canonical = False).ToList()
                    Dim iRow = DataGridViewUrlAltSegments.Rows.Add(segment.Segment)
                    Dim oRow = DataGridViewUrlAltSegments.Rows(iRow)
                    Dim src As String = segment.Segment
                    oRow.Cells(0).Style.BackColor = If(src = src.Urlfy, Color.White, Color.LightSalmon)
                Next

                CheckUrlfy()

                TextBoxDefaultUrlEsp.Text = .UrlCanonicas.RelativeUrl(DTOLang.ESP) '.Urls.Url(DTOLang.ESP, False)
                TextBoxDefaultUrlCat.Text = .UrlCanonicas.RelativeUrl(DTOLang.CAT) '.Urls.Url(DTOLang.CAT, False)
                TextBoxDefaultUrlEng.Text = .UrlCanonicas.RelativeUrl(DTOLang.ENG) '.Urls.Url(DTOLang.ENG, False)
                TextBoxDefaultUrlPor.Text = .UrlCanonicas.RelativeUrl(DTOLang.POR) '.Urls.Url(DTOLang.POR, False)

                TextBoxCanonicalUrlEsp.Text = .UrlCanonicas.CanonicalUrl(DTOLang.ESP) '.Urls.Canonical(DTOLang.ESP, True)
                TextBoxCanonicalUrlCat.Text = .UrlCanonicas.CanonicalUrl(DTOLang.CAT) '.Urls.Canonical(DTOLang.CAT, True)
                TextBoxCanonicalUrlEng.Text = .UrlCanonicas.CanonicalUrl(DTOLang.ENG) '.Urls.Canonical(DTOLang.ENG, True)
                TextBoxCanonicalUrlPor.Text = .UrlCanonicas.CanonicalUrl(DTOLang.POR) '.Urls.Canonical(DTOLang.POR, True)

                If TypeOf _Product Is DTOProductSku Then
                    Dim oSku As DTOProductSku = _Product
                    With oSku.NomLlarg
                        TextBoxNomLlargEsp.Text = .Esp
                        TextBoxNomLlargCat.Text = .Cat
                        TextBoxNomLlargEng.Text = .Eng
                        TextBoxNomLlargPor.Text = .Por
                    End With
                    CheckBoxInheritExtracte.Checked = oSku.Hereda
                    CheckBoxInheritContent.Visible = oSku.Hereda
                    GroupBoxNom.Text = "Nom curt (per combinar amb el noms de marca i categoria)"
                    GroupBoxNomLlarg.Visible = True
                Else
                    CheckBoxInheritExtracte.Visible = False
                    CheckBoxInheritContent.Visible = False

                    Xl_LangsTextDsc.Dock = DockStyle.Fill
                End If

            End With
        Else
            UIHelper.WarnError(exs)
        End If
        _AllowEvents = True
    End Sub

    Private Sub CheckUrlfy()
        WarnUrlTextbox(TextBoxUrlEsp)
        WarnUrlTextbox(TextBoxUrlCat)
        WarnUrlTextbox(TextBoxUrlEng)
        WarnUrlTextbox(TextBoxUrlPor)

        WarnUrlTextbox(TextBoxDefaultUrlEsp)
        WarnUrlTextbox(TextBoxDefaultUrlCat)
        WarnUrlTextbox(TextBoxDefaultUrlEng)
        WarnUrlTextbox(TextBoxDefaultUrlPor)

        WarnUrlTextbox(TextBoxCanonicalUrlEsp)
        WarnUrlTextbox(TextBoxCanonicalUrlCat)
        WarnUrlTextbox(TextBoxCanonicalUrlEng)
        WarnUrlTextbox(TextBoxCanonicalUrlPor)


    End Sub

    Private Sub WarnUrlTextbox(oTextBox As TextBox)
        oTextBox.BackColor = If(oTextBox.Text = oTextBox.Text.Urlfy, Color.White, Color.LightSalmon)
    End Sub

    Private Sub DataGridViewUrlAltSegments_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewUrlAltSegments.CellEndEdit
        Dim oGrid As DataGridView = CType(sender, DataGridView)
        Dim oCell = oGrid.Rows(e.RowIndex).Cells(e.ColumnIndex)
        oCell.Style.BackColor = If(oCell.FormattedValue.ToString = oCell.FormattedValue.ToString.Urlfy, Color.White, Color.LightSalmon)
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
            Xl_LangsTextDsc.AfterUpdate,
            TextBoxNomLlargEsp.TextChanged,
            TextBoxNomLlargCat.TextChanged,
            TextBoxNomLlargEng.TextChanged,
            TextBoxNomLlargPor.TextChanged,
            DataGridViewUrlAltSegments.RowsAdded,
            DataGridViewUrlAltSegments.RowsRemoved,
            DataGridViewUrlAltSegments.Validated

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Url_Changed(sender As Object, e As EventArgs) Handles _
            TextBoxUrlEsp.TextChanged,
            TextBoxUrlCat.TextChanged,
            TextBoxUrlEng.TextChanged,
            TextBoxUrlPor.TextChanged

        If _AllowEvents Then
            CheckUrlfy()
            ButtonOk.Enabled = True
        End If
    End Sub


    Private Sub TextBoxNomEsp_TextChanged(sender As Object, e As EventArgs) Handles TextBoxNomEsp.TextChanged
        If _Product.IsNew Then
            TextBoxUrlEsp.Text = TextBoxNomEsp.Text.Urlfy
        Else
            _DirtyUrls = True
        End If
        ButtonOk.Enabled = True
    End Sub

    Private Sub TextBoxNomCat_TextChanged(sender As Object, e As EventArgs) Handles TextBoxNomCat.TextChanged
        If _Product.IsNew Then
            TextBoxUrlCat.Text = TextBoxNomCat.Text.Urlfy
        Else
            _DirtyUrls = True
        End If
        ButtonOk.Enabled = True
    End Sub

    Private Sub TextBoxNomEng_TextChanged(sender As Object, e As EventArgs) Handles TextBoxNomEng.TextChanged
        If _Product.IsNew Then
            TextBoxUrlEng.Text = TextBoxNomEng.Text.Urlfy
        Else
            _DirtyUrls = True
        End If
        ButtonOk.Enabled = True
    End Sub

    Private Sub TextBoxNomPor_TextChanged(sender As Object, e As EventArgs) Handles TextBoxNomPor.TextChanged
        If _Product.IsNew Then
            TextBoxUrlPor.Text = TextBoxNomPor.Text.Urlfy
        Else
            _DirtyUrls = True
        End If
        ButtonOk.Enabled = True
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        If _DirtyUrls Then
            Dim sb As New Text.StringBuilder
            If TextBoxUrlEsp.Text = "" Then
                TextBoxUrlEsp.Text = TextBoxNomEsp.Text.Urlfy
            ElseIf TextBoxUrlEsp.Text <> TextBoxNomEsp.Text.Urlfy Then
                sb.AppendLine("url espanyol: " & TextBoxUrlEsp.Text)
                sb.AppendLine("url suggerida: " & TextBoxNomEsp.Text.Urlfy)
            End If

            If TextBoxUrlCat.Text = "" Then
                TextBoxUrlCat.Text = TextBoxNomCat.Text.Urlfy
            ElseIf TextBoxUrlCat.Text <> TextBoxNomCat.Text.Urlfy Then
                sb.AppendLine("url Català: " & TextBoxUrlCat.Text)
                sb.AppendLine("url suggerida: " & TextBoxNomCat.Text.Urlfy)
            End If

            If TextBoxUrlEng.Text = "" Then
                TextBoxUrlEng.Text = TextBoxNomEng.Text.Urlfy
            ElseIf TextBoxUrlEng.Text <> TextBoxNomEng.Text.Urlfy Then
                sb.AppendLine("url Anglès: " & TextBoxUrlEng.Text)
                sb.AppendLine("url suggerida: " & TextBoxNomEng.Text.Urlfy)
            End If

            If TextBoxUrlPor.Text = "" Then
                TextBoxUrlPor.Text = TextBoxNomPor.Text.Urlfy
            ElseIf TextBoxUrlPor.Text <> TextBoxNomPor.Text.Urlfy Then
                sb.AppendLine("url Portuguès: " & TextBoxUrlPor.Text)
                sb.AppendLine("url suggerida: " & TextBoxNomPor.Text.Urlfy)
            End If

            If sb.Length > 0 Then
                sb.Insert(0, "Actualitzem les url canóniques amb els nous noms?" & vbCrLf)
                Dim rc = MsgBox(sb.ToString, MsgBoxStyle.YesNoCancel)
                Select Case rc
                    Case MsgBoxResult.Yes
                        TextBoxUrlEsp.Text = TextBoxNomEsp.Text.Urlfy
                        TextBoxUrlCat.Text = TextBoxNomCat.Text.Urlfy
                        TextBoxUrlEng.Text = TextBoxNomEng.Text.Urlfy
                        TextBoxUrlPor.Text = TextBoxNomPor.Text.Urlfy
                    Case MsgBoxResult.No
                    Case MsgBoxResult.Cancel
                        Exit Sub
                End Select
            End If
        End If


        If _Product.IsNew Then
            'prevent saving until original calling form has been completed
            ReadFromForm()
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Product))
            Me.Close()
        Else
            'save and return to main form
            Dim exs As New List(Of Exception)
            UIHelper.ToggleProggressBar(Panel1, True)


            If TypeOf _Product Is DTOProductBrand Then
                Dim oBrand As DTOProductBrand = _Product
                If FEB2.ProductBrand.Load(oBrand, exs) Then
                    ReadFromForm()
                    If Await FEB2.ProductBrand.Update(oBrand, exs) Then
                        RaiseEvent AfterUpdate(Me, New MatEventArgs(oBrand))
                        Me.Close()
                    Else
                        UIHelper.ToggleProggressBar(Panel1, False)
                        UIHelper.WarnError(exs)
                    End If
                Else
                    UIHelper.ToggleProggressBar(Panel1, False)
                    UIHelper.WarnError(exs)
                End If
            ElseIf TypeOf _Product Is DTOProductCategory Then
                Dim oCategory As DTOProductCategory = _Product
                If FEB2.ProductCategory.Load(oCategory, exs) Then
                    ReadFromForm()
                    If Await FEB2.ProductCategory.Update(oCategory, exs) Then
                        RaiseEvent AfterUpdate(Me, New MatEventArgs(_Product))
                        Me.Close()
                    Else
                        UIHelper.ToggleProggressBar(Panel1, False)
                        UIHelper.WarnError(exs)
                    End If
                Else
                    UIHelper.ToggleProggressBar(Panel1, False)
                    UIHelper.WarnError(exs)
                End If
            ElseIf TypeOf _Product Is DTODept Then
                Dim oDept As DTODept = _Product
                If FEB2.Dept.Load(oDept, False, exs) Then
                    ReadFromForm()
                    If Await FEB2.Dept.Upload(oDept, exs) Then
                        RaiseEvent AfterUpdate(Me, New MatEventArgs(_Product))
                        Me.Close()
                    Else
                        UIHelper.ToggleProggressBar(Panel1, False)
                        UIHelper.WarnError(exs)
                    End If
                Else
                    UIHelper.ToggleProggressBar(Panel1, False)
                    UIHelper.WarnError(exs)
                End If
            ElseIf TypeOf _Product Is DTOProductSku Then
                Dim oSku As DTOProductSku = _Product
                If FEB2.ProductSku.Load(oSku, exs) Then
                    ReadFromForm()
                    If Await FEB2.ProductSku.Update(oSku, exs) Then
                        RaiseEvent AfterUpdate(Me, New MatEventArgs(_Product))
                        Me.Close()
                    Else
                        UIHelper.ToggleProggressBar(Panel1, False)
                        UIHelper.WarnError(exs)
                    End If
                Else
                    UIHelper.ToggleProggressBar(Panel1, False)
                    UIHelper.WarnError(exs)
                End If
            End If
        End If

    End Sub

    Private Sub ReadFromForm()
        With _Product
            With .Excerpt
                .Esp = TextBoxExcerptEsp.Text
                .Cat = TextBoxExcerptCat.Text
                .Eng = TextBoxExcerptEng.Text
                .Por = TextBoxExcerptPor.Text
            End With
            With .Nom
                .Esp = TextBoxNomEsp.Text
                .Cat = TextBoxNomCat.Text
                .Eng = TextBoxNomEng.Text
                .Por = TextBoxNomPor.Text
            End With
            .Content = Xl_LangsTextDsc.Value

            .UrlSegments = New DTOUrlSegment.Collection
            With .UrlSegments
                If TextBoxUrlEsp.Text.isNotEmpty Then .Add(DTOUrlSegment.Factory(TextBoxUrlEsp.Text, DTOLang.ESP, True))
                If TextBoxUrlCat.Text.isNotEmpty Then .Add(DTOUrlSegment.Factory(TextBoxUrlCat.Text, DTOLang.CAT, True))
                If TextBoxUrlEng.Text.isNotEmpty Then .Add(DTOUrlSegment.Factory(TextBoxUrlEng.Text, DTOLang.ENG, True))
                If TextBoxUrlPor.Text.isNotEmpty Then .Add(DTOUrlSegment.Factory(TextBoxUrlPor.Text, DTOLang.POR, True))

                For Each oRow In DataGridViewUrlAltSegments.Rows
                    Dim src As String = oRow.cells(0).Value
                    If src.isNotEmpty Then
                        .Add(DTOUrlSegment.Factory(src))
                    End If
                Next
            End With

            If TypeOf _Product Is DTOProductSku Then
                With CType(_Product, DTOProductSku)
                    With .NomLlarg
                        .Esp = TextBoxNomLlargEsp.Text
                        .Cat = TextBoxNomLlargCat.Text
                        .Eng = TextBoxNomLlargEng.Text
                        .Por = TextBoxNomLlargPor.Text
                    End With
                    .hereda = CheckBoxInheritExtracte.Checked
                End With
            End If
        End With

    End Sub

    Private Sub CheckBoxInheritExtracte_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxInheritExtracte.CheckedChanged, CheckBoxInheritContent.CheckedChanged
        If _AllowEvents Then
            _AllowEvents = False
            Dim checked = CType(sender, CheckBox).Checked
            CheckBoxInheritExtracte.Checked = checked
            CheckBoxInheritContent.Checked = checked
            ButtonOk.Enabled = True
            _AllowEvents = True
        End If
    End Sub

    Private Sub TextBoxExcerptEsp_TextChanged(sender As Object, e As EventArgs) Handles TextBoxExcerptEsp.TextChanged
        Dim oTextbox As TextBox = sender
        LabelExtracteCountEsp.Visible = oTextbox.Text.Length <> 0
        LabelExtracteCountEsp.Text = oTextbox.Text.Length
        oTextbox.BackColor = If(oTextbox.Text.Length > maxLengthExtracte, Color.Yellow, Color.White)
        CheckExtracteWarning()
    End Sub
    Private Sub TextBoxExcerptCat_TextChanged(sender As Object, e As EventArgs) Handles TextBoxExcerptCat.TextChanged
        Dim oTextbox As TextBox = sender
        LabelExtracteCountCat.Visible = oTextbox.Text.Length <> 0
        LabelExtracteCountCat.Text = oTextbox.Text.Length
        oTextbox.BackColor = If(oTextbox.Text.Length > maxLengthExtracte, Color.Yellow, Color.White)
        CheckExtracteWarning()
    End Sub
    Private Sub TextBoxExcerptEng_TextChanged(sender As Object, e As EventArgs) Handles TextBoxExcerptEng.TextChanged
        Dim oTextbox As TextBox = sender
        LabelExtracteCountEng.Visible = oTextbox.Text.Length <> 0
        LabelExtracteCountEng.Text = oTextbox.Text.Length
        oTextbox.BackColor = If(oTextbox.Text.Length > maxLengthExtracte, Color.Yellow, Color.White)
        CheckExtracteWarning()
    End Sub
    Private Sub TextBoxExcerptPor_TextChanged(sender As Object, e As EventArgs) Handles TextBoxExcerptPor.TextChanged
        Dim oTextbox As TextBox = sender
        LabelExtracteCountPor.Visible = oTextbox.Text.Length <> 0
        LabelExtracteCountPor.Text = oTextbox.Text.Length
        oTextbox.BackColor = If(oTextbox.Text.Length > maxLengthExtracte, Color.Yellow, Color.White)
        CheckExtracteWarning()
    End Sub

    Private Sub CheckExtracteWarning()
        Dim oLangs As New List(Of DTOLang)
        GroupBoxWarningExtracte.Visible = (TextBoxExcerptEsp.Text.Length > maxLengthExtracte Or TextBoxExcerptCat.Text.Length > maxLengthExtracte Or TextBoxExcerptEng.Text.Length > maxLengthExtracte Or TextBoxExcerptPor.Text.Length > maxLengthExtracte)
    End Sub


End Class