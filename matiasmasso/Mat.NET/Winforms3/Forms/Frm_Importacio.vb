Public Class Frm_Importacio

    Private _Importacio As DTOImportacio
    'Private mMgz As DTOMgz
    Private mNewRemesa As Boolean
    Private mDirtyCell As Boolean
    Private mDsArts As DataSet
    Private mDsDocs As DataSet
    Private mLastValidatedObject As Object
    Private _Tab As Tabs
    Private _TabLoaded(10) As Boolean
    'Private mEmp as DTOEmp
    Private _DocFile As DTODocFile
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Private Enum ColsArts
        Guid
        Ico
        Id
        Nom
        Qty
        Cfm
    End Enum

    Private Enum ColsDocs
        Src
        Guid
        Ico
        Txt
        Hash
    End Enum

    Public Enum Tabs
        Gral
        Docs
        Previsio
        Validacio
    End Enum


    Public Sub New(ByVal oImportacio As DTOImportacio, Optional oTab As Tabs = Tabs.Gral)
        MyBase.New()
        Me.InitializeComponent()
        Me.AllowDrop = True

        _Importacio = oImportacio
        _Tab = oTab
    End Sub


    Private Async Sub Frm_Importacio_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Importacio.Load(exs, _Importacio) Then
            If Await LoadIncoterms(exs) Then
                Await SetImportacio()
                _AllowEvents = True

                If _Tab <> Tabs.Gral Then
                    TabControl1.SelectedIndex = _Tab

                    Select Case TabControl1.SelectedIndex
                        Case Tabs.Previsio
                            Await RefrescaPrevisions()
                    End Select
                    _TabLoaded(TabControl1.SelectedIndex) = True

                End If
            Else
                UIHelper.WarnError(exs)
            End If

            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Function SetImportacio() As Task
        If _Importacio.IsNew Then
            mNewRemesa = True
            Me.Text = "Nova remesa d'importació"
            NumericUpDownYea.Value = DTO.GlobalVariables.Today().Year
            DateTimePickerETD.Value = DTO.GlobalVariables.Today()
            DateTimePickerEta.Value = DTO.GlobalVariables.Today()
            If _Importacio.proveidor IsNot Nothing Then
                Xl_LookupIncoterm1.Value = _Importacio.proveidor.IncoTerm
            End If
            EnableButton()
            ButtonDel.Enabled = False
        Else
            Me.Text = "Remesa d'importació num." & _Importacio.id
            NumericUpDownYea.ReadOnly = True
            DateTimePickerEta.Value = _Importacio.fchETA
            If _Importacio.fchETD > DateTimePickerETD.MinDate Then
                DateTimePickerETD.Value = _Importacio.fchETD
            End If
            'EnableButton()
            ButtonDel.Enabled = True
        End If
        Await RefrescaGral()
        Xl_ImportacioDocs1.Load(_Importacio)

    End Function


    Private Async Function RefrescaGral() As Task
        Dim exs As New List(Of Exception)
        With _Importacio
            NumericUpDownYea.Value = .yea
            NumericUpDownWeek.Value = .week
            CheckBoxArrived.Checked = .arrived
            LabelUpDownWeek.Visible = Not CheckBoxArrived.Checked
            NumericUpDownWeek.Visible = Not CheckBoxArrived.Checked
            Await Xl_ContactPrv.Load(exs, .proveidor)
            Await Xl_ContactPlataformaDeCarga.Load(exs, .plataformaDeCarga)
            Await Xl_ContactTrp.Load(exs, .transportista)
            If .fchAvisTrp > DateTimePickerOrdenDeCarga.MinDate Then
                CheckBoxOrdenDeCarga.Checked = True
                DateTimePickerOrdenDeCarga.Visible = True
                DateTimePickerOrdenDeCarga.Value = .fchAvisTrp
            Else
                CheckBoxOrdenDeCarga.Checked = False
                DateTimePickerOrdenDeCarga.Visible = False
            End If

            TextBoxMatricula.Text = .matricula
            TextBoxBultos.Text = .bultos
            TextBoxKg.Text = .kg
            TextBoxM3.Text = .m3
            TextBoxObs.Text = .obs
            If .amt IsNot Nothing Then TextBoxAmt.Text = DTOAmt.CurFormatted(.amt)
            TextBoxGoods.Text = Format(.goods, "#,###.## €")
            Xl_LookupIncoterm1.Value = .incoTerm
            Xl_PaisOrigen.Country = .countryOrigen
            CheckBoxDisabled.Checked = .disabled
        End With
    End Function

    Private Sub Frm_Importacio_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
        Dim fileNames() As String = Nothing

        Try
            If e.Data.GetDataPresent(DataFormats.FileDrop, False) Then
                fileNames = e.Data.GetData(DataFormats.FileDrop)
                Dim sFilename As String = fileNames(0)
                ' get the actual raw file into memory
                Dim oFileStream As New System.IO.FileStream(sFilename, IO.FileMode.Open)
                ' allocate enough bytes to hold the raw data
                Dim oBinaryReader As New IO.BinaryReader(oFileStream)
                Dim oStream As Byte() = oBinaryReader.ReadBytes(oFileStream.Length)
                oBinaryReader.Close()

                Dim exs As New List(Of Exception)
                ' If TabControl1.SelectedIndex = Tabs.Validacio Then
                'Dim oValidacions As List(Of DTOImportValidacio) = BLLImportValidacions.Validacions(Current.Session.Emp, oStream, exs)
                'If exs.Count > 0 Then
                'UIHelper.WarnError(exs)
                'Else
                'If ImportaValidacions(oValidacions, exs) Then
                'RefrescaValidacions()
                'Else
                'UIHelper.WarnError(exs)
                'End If
                'End If
                'End If

            ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
                '
                ' the first step here is to get the filename
                ' of the attachment and
                ' build a full-path name so we can store it 
                ' in the temporary folder
                '
                ' set up to obtain the FileGroupDescriptor 
                ' and extract the file name
                Dim theStream As System.IO.MemoryStream = e.Data.GetData("FileGroupDescriptor")
                Dim fileGroupDescriptor(512) As Byte
                theStream.Read(fileGroupDescriptor, 0, 512)

                ' used to build the filename from the FileGroupDescriptor block
                Dim sfilename As String = ""
                For i As Integer = 76 To 512
                    If fileGroupDescriptor(i) = 0 Then Exit For
                    sfilename = sfilename & Convert.ToChar(fileGroupDescriptor(i))
                Next
                theStream.Close()

                '
                ' Second step:  we have the file name.  
                ' Now we need to get the actual raw
                ' data for the attached file .
                '

                ' get the actual raw file into memory
                Dim oMemStream As System.IO.MemoryStream = e.Data.GetData("FileContents", True)
                ' allocate enough bytes to hold the raw data
                Dim oBinaryReader As New IO.BinaryReader(oMemStream)
                Dim oStream As Byte() = oBinaryReader.ReadBytes(oMemStream.Length)
                oBinaryReader.Close()

                Dim exs As New List(Of Exception)
                'Dim oValidacions As List(Of DTOImportValidacio) = BLLImportValidacions.Validacions(Current.Session.Emp, oStream, exs)
                'If exs.Count > 0 Then
                'UIHelper.WarnError(exs)
                'Else
                'If ImportaValidacions(oValidacions, exs) Then
                'RefrescaValidacions()
                'Else
                'UIHelper.WarnError(exs)
                'End If
                'End If

            Else
                MsgBox("format desconegut")
            End If
        Catch ex As Exception
            MsgBox("Error in DragDrop function: " + ex.Message)
        End Try
    End Sub



    Private Sub Frm_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
            '    or this tells us if it is an Outlook attachment drop
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            e.Effect = DragDropEffects.Copy
            '    or none of the above
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Importacio
            .Week = NumericUpDownWeek.Value
            .Proveidor = DTOProveidor.FromContact(Xl_ContactPrv.Contact)
            .PlataformaDeCarga = Xl_ContactPlataformaDeCarga.Contact
            .Transportista = DTOTransportista.FromContact(Xl_ContactTrp.Contact)
            .Yea = NumericUpDownYea.Value
            .FchETD = DateTimePickerETD.Value
            .FchETA = DateTimePickerEta.Value
            .Arrived = CheckBoxArrived.Checked
            .FchAvisTrp = IIf(CheckBoxOrdenDeCarga.Checked, DateTimePickerOrdenDeCarga.Value, Nothing)
            .Matricula = TextBoxMatricula.Text
            .Bultos = IIf(IsNumeric(TextBoxBultos.Text), TextBoxBultos.Text, 0)
            .Kg = IIf(IsNumeric(TextBoxKg.Text), TextBoxKg.Text, 0)
            .M3 = IIf(IsNumeric(TextBoxM3.Text), TextBoxM3.Text, 0)
            .Obs = TextBoxObs.Text
            .Disabled = CheckBoxDisabled.Checked
            .incoTerm = Xl_LookupIncoterm1.Value

            .CountryOrigen = Xl_PaisOrigen.Country
            If Xl_ImportPrevisio1.IsDirty Then
                .Previsions = Xl_ImportPrevisio1.Values
            End If
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        Dim id = Await FEB.Importacio.Update(_Importacio, exs)
        If exs.Count = 0 Then
            _Importacio.id = id
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Importacio))
            If mNewRemesa Then
                UIHelper.ToggleProggressBar(Panel1, False)
                MsgBox("remesa registrada amb el numero " & _Importacio.id)
            End If
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            MsgBox("error al desar la remesa " & _Importacio.Id & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub


    Private Sub Xl_ContactPrv_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_ContactPrv.AfterUpdate
        Dim exs As New List(Of Exception)
        If _AllowEvents Then
            Dim oProveidor As New DTOProveidor(Xl_ContactPrv.Contact.Guid)
            If FEB.Contact.Load(oProveidor, exs) Then
                If FEB.Proveidor.Load(oProveidor, exs) Then
                    With oProveidor
                        Xl_LookupIncoterm1.Value = .IncoTerm
                        Xl_PaisOrigen.Country = DTOAddress.Country(oProveidor.Address)
                    End With
                    EnableButton()
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub


    Private Sub Control_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
     TextBoxBultos.TextChanged,
      TextBoxKg.TextChanged,
       TextBoxM3.TextChanged,
        TextBoxObs.TextChanged,
         Xl_PaisOrigen.AfterUpdate,
           Xl_LookupIncoterm1.AfterUpdate,
            NumericUpDownWeek.ValueChanged,
             TextBoxMatricula.TextChanged,
              CheckBoxArrived.CheckedChanged,
               DateTimePickerEta.ValueChanged,
                DateTimePickerETD.ValueChanged,
                  Xl_ContactTrp.AfterUpdate,
                   CheckBoxOrdenDeCarga.CheckedChanged,
                    DateTimePickerOrdenDeCarga.ValueChanged,
                     Xl_ContactPlataformaDeCarga.AfterUpdate,
                      CheckBoxDisabled.CheckedChanged

        If _AllowEvents Then EnableButton()
    End Sub

    Private Sub DateTimePickerEta_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePickerEta.ValueChanged
        If _AllowEvents Then
            Dim DtFch As Date = DateTimePickerEta.Value
            Dim oWeek As Integer = MatHelperStd.TimeHelper.WeekNumber(DtFch)
            NumericUpDownWeek.Value = oWeek
            EnableButton()
        End If
    End Sub

    Private Sub EnableButton()
        ButtonOk.Enabled = True
    End Sub

    Private Sub ToolStripButtonExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oSheet = MatHelper.Excel.Sheet.Factory(mDsArts)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function LoadIncoterms(exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim oIncoterms As List(Of DTOIncoterm) = Await FEB.Incoterms.All(exs)
        If exs.Count = 0 Then
            Xl_LookupIncoterm1.Load(oIncoterms)
            retval = True
        End If
        Return retval
    End Function



    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem la remesa " & _Importacio.Id & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.Importacio.Delete(_Importacio, exs) Then
                RaiseEvent AfterUpdate(_Importacio, MatEventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar la remesa")
            End If
        End If
    End Sub

    Private Sub Xl_ImportacioDocs1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_ImportacioDocs1.AfterUpdate
        If _AllowEvents Then
            TextBoxAmt.Text = DTOAmt.CurFormatted(DTOImportacio.CostMercancia(_Importacio))
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Importacio))
            EnableButton()
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If Not _TabLoaded(TabControl1.SelectedIndex) Then
            Select Case TabControl1.SelectedIndex
                Case Tabs.Previsio
                    Await RefrescaPrevisions()
                Case Tabs.Validacio
                    RefrescaValidacions()
            End Select
            _TabLoaded(TabControl1.SelectedIndex) = True
        End If
    End Sub

    Private Async Function RefrescaPrevisions() As Task
        Dim exs As New List(Of Exception)
        If _Importacio.previsions.Count = 0 Then _Importacio = Await FEB.ImportPrevisions.Load(exs, _Importacio)
        If exs.Count = 0 Then
            Xl_ImportPrevisio1.Load(_Importacio.Previsions)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub RefrescaValidacions()
        'BLLImportValidacions.Load(_Importacio)
        'Xl_ImportValidacions1.Load(_Importacio.Validacions)
    End Sub


    Private Sub Xl_ImportPrevisio1_RequestToImportPrevisio(sender As Object, e As MatEventArgs) Handles Xl_ImportPrevisio1.RequestToImportPrevisio
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Importar previsió"
            .Filter = "fitxers Excel|*.xls;*.xlsx|tots els fitxers|*.*"
            If .ShowDialog Then
                If .FileName > "" Then
                    Dim exs As New List(Of Exception)
                    If FileSystemHelper.IsFileLocked(.FileName, IO.FileMode.Open, IO.FileAccess.Read, exs) Then
                        UIHelper.WarnError(exs)
                    Else
                        'Dim oExcel As MatHelper.Excel.Book = MatExcel.Read2(.FileName)
                        Dim sFields() As String = {"Quantitat", "Ref.Proveidor", "Descripcio", "Comanda"}
                        Dim oFrm As New Frm_ExcelColumsMapping(sFields, .FileName)
                        AddHandler oFrm.AfterUpdate, AddressOf Do_ImportaPrevisio
                        oFrm.Show()
                    End If
                End If
            End If
        End With
    End Sub


    Private Async Sub Do_ImportaPrevisio(sender As Object, e As MatEventArgs)
        Dim oSheet As MatHelper.Excel.Sheet = e.Argument
        Dim exs As New List(Of Exception)
        If Await FEB.ImportPrevisions.UploadExcel(exs, _Importacio, oSheet) Then
            Await RefrescaPrevisions()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub Xl_ImportPrevisio1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ImportPrevisio1.RequestToRefresh
        Await RefrescaPrevisions()
    End Sub

    Private Async Sub Xl_ImportPrevisio1_onPurchaseOrderItemUpdateRequest(sender As Object, e As MatEventArgs) Handles Xl_ImportPrevisio1.onPurchaseOrderItemUpdateRequest
        Dim exs As New List(Of Exception)
        Dim oPrevisions As List(Of DTOImportPrevisio) = _Importacio.Previsions

        Dim oPrevisio As DTOImportPrevisio = e.Argument
        If Not String.IsNullOrEmpty(oPrevisio.NumComandaProveidor) Then
            Dim oMatchingPrevisions As List(Of DTOImportPrevisio) = oPrevisions.Where(Function(x) x.NumComandaProveidor = oPrevisio.NumComandaProveidor And x.UnEquals(oPrevisio)).ToList
            Dim oPncs = Await FEB.PurchaseOrderItems.Pending(exs, _Importacio.Proveidor, DTOPurchaseOrder.Codis.Proveidor, GlobalVariables.Emp.Mgz)
            oPncs = oPncs.Where(Function(y) y.PurchaseOrder.Equals(oPrevisio.PurchaseOrderItem.PurchaseOrder)).ToList

            For Each item As DTOImportPrevisio In oMatchingPrevisions
                Dim oPnc As DTOPurchaseOrderItem = oPncs.Find(Function(x) x.Sku.Equals(item.Sku) And x.Pending = item.Qty)
                If oPnc IsNot Nothing Then
                    item.PurchaseOrderItem = oPnc
                End If
            Next

        End If


        If Await FEB.ImportPrevisions.Update(exs, _Importacio) Then
            Await RefrescaPrevisions()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub Xl_ImportPrevisio1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_ImportPrevisio1.AfterUpdate
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxArrived_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxArrived.CheckedChanged
        LabelUpDownWeek.Visible = Not CheckBoxArrived.Checked
        NumericUpDownWeek.Visible = Not CheckBoxArrived.Checked
    End Sub

    Private Sub CheckBoxOrdenDeCarga_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxOrdenDeCarga.CheckedChanged
        If _AllowEvents Then
            DateTimePickerOrdenDeCarga.Visible = CheckBoxOrdenDeCarga.Checked
        End If
    End Sub

End Class