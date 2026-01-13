Public Class Frm_ProductDownload
    Private _ProductDownload As DTOProductDownload
    Private _AllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oProductDownload As DTOProductDownload)
        MyBase.New()
        Me.InitializeComponent()

        _ProductDownload = oProductDownload
    End Sub

    Private Async Sub Frm_ProductDownload_LoadAsync(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _ProductDownload.IsNew Then
            LoadSrcs()
            Await Refresca()
            _AllowEvents = True
        Else
            _ProductDownload = Await FEB.Download.Find(_ProductDownload.Guid, exs)
            If exs.Count = 0 Then
                LoadSrcs()
                Await Refresca()
                _AllowEvents = True
            Else
                UIHelper.WarnError(exs)
                Me.Close()
            End If
        End If
    End Sub

    Private Async Function Refresca() As Task

        With _ProductDownload
            If .DocFile IsNot Nothing Then
                TextBoxNom.Text = .DocFile.Nom
            End If
            CheckBoxPublicarAlConsumidor.Checked = .PublicarAlConsumidor
            CheckBoxPublicarAlDistribuidor.Checked = .PublicarAlDistribuidor

            If .Lang IsNot Nothing Then
                CheckBoxLang.Checked = True
                Xl_Langs1.Enabled = True
                Xl_Langs1.Value = .Lang
            End If

            Xl_LangSet1.Load(.LangSet)

            CheckBoxObsolet.Checked = .Obsoleto
            Await Xl_DocFile1.Load(.DocFile)

            If Not .Src = DTOProductDownload.Srcs.notSet Then
                For Each Itm As ListItem In ComboBoxSrcs.Items
                    If Itm.Value = .Src Then
                        ComboBoxSrcs.SelectedItem = Itm
                        Exit For
                    End If
                Next
                ButtonDel.Enabled = True
            End If

            Xl_BaseGuidCodNoms1.Load(.Targets)

            If .DocFile IsNot Nothing Then
                TextBoxNom.Text = .DocFile.Nom
            End If
        End With

    End Function

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxNom.TextChanged,
        CheckBoxObsolet.CheckedChanged,
         Xl_DocFile1.AfterUpdate,
          ComboBoxSrcs.SelectedIndexChanged,
           CheckBoxPublicarAlConsumidor.CheckedChanged,
            CheckBoxPublicarAlDistribuidor.CheckedChanged,
             Xl_Langs1.AfterUpdate,
              Xl_LangSet1.AfterUpdate,
               Xl_BaseGuidCodNoms1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxLang_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxLang.CheckedChanged
        If _AllowEvents Then
            Xl_Langs1.Enabled = CheckBoxLang.Checked
            ButtonOk.Enabled = True
        End If
    End Sub


    Private Async Sub ButtonOk_ClickAsync(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oSelectedItem As ListItem = DirectCast(ComboBoxSrcs.SelectedItem, ListItem)
        If oSelectedItem.Value = DTOProductDownload.Srcs.NotSet Then
            MsgBox("falta triar una categoría", MsgBoxStyle.Exclamation, "MAT.NET")
        Else
            Dim oDocfile As DTODocFile = Nothing
            If Xl_Docfile1.Value IsNot Nothing Then
                oDocfile = Xl_Docfile1.Value
                With oDocfile
                    .Nom = TextBoxNom.Text
                End With
            End If
            With _ProductDownload
                .Src = DirectCast(ComboBoxSrcs.SelectedItem, ListItem).Value
                If CheckBoxLang.Checked Then
                    .Lang = Xl_Langs1.Value
                Else
                    .Lang = Nothing
                End If
                .LangSet = Xl_LangSet1.Value
                .PublicarAlConsumidor = CheckBoxPublicarAlConsumidor.Checked
                .PublicarAlDistribuidor = CheckBoxPublicarAlDistribuidor.Checked
                .Obsoleto = CheckBoxObsolet.Checked
                .Targets = Xl_BaseGuidCodNoms1.Values
                .DocFile = oDocfile
            End With

            Dim exs As New List(Of Exception)
            UIHelper.ToggleProggressBar(Panel1, True)
            If Await FEB.Download.Upload(_ProductDownload, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductDownload))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If Await FEB.Download.Delete(_ProductDownload, exs) Then
            RaiseEvent AfterUpdate(_ProductDownload, New MatEventArgs(_ProductDownload))
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub LoadSrcs()
        For Each v As DTOProductDownload.Srcs In [Enum].GetValues(GetType(DTOProductDownload.Srcs))
            Dim oItem As New ListItem(v, v.ToString.Replace("_", " "))
            If v = DTOProductDownload.Srcs.NotSet Then oItem.Text = "(seleccionar categoria)"
            ComboBoxSrcs.Items.Add(oItem)
        Next
    End Sub


    Private Sub Xl_BaseGuidCodNoms1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_BaseGuidCodNoms1.RequestToAddNew
        Dim oFrm As New Frm_Catalog(DTOProduct.SelectionModes.SelectAny)
        AddHandler oFrm.OnItemSelected, AddressOf AddNewTarget
        oFrm.Show()
    End Sub

    Private Sub AddNewTarget(sender As Object, e As MatEventArgs)
        Dim oProduct As DTOProduct = e.Argument
        Dim oTarget As New DTOBaseGuidCodNom(oProduct.Guid)
        oTarget.cod = oProduct.SourceCod
        If oTarget.cod = DTOBaseGuidCodNom.Cods.productSku Then
            oTarget.nom = oProduct.NomLlarg.Tradueix(Current.Session.Lang)
        Else
            oTarget.nom = oProduct.Nom.Tradueix(Current.Session.Lang)
        End If
        _ProductDownload.Targets.Add(oTarget)
        Xl_BaseGuidCodNoms1.Load(_ProductDownload.Targets)
        ButtonOk.Enabled = True
    End Sub
End Class