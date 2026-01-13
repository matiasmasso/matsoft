Module TestVivace

    Public Function Prueba() As Boolean
        Dim sUrl As String = "https://matiasmasso-api.azurewebsites.net/api/vivace/outvivace/pruebas"
        'Dim sUrl As String = "http://localhost:52887/api/vivace/outvivace/pruebas"
        'Dim sJsonInputString As String = "{'fecha':'10-02-2017','remite':{'nif':'A62572342','nombre':'Vivace Logistica'},'destino':{'nif':'A58007857','nombre':'MATIAS MASSO, S.A.'},'expediciones':[{'vivace':'2017/22614','bultos':3,'m3':0.123,'kg':24,'transpnif':'A28815017','albaranes':[{'numero':'2017011792','cargos':[{'codigo':'METCIVR2','unidades':9,'precio':0.09},{'codigo':'EMB4VR2','unidades':1,'precio':0.34}]}],'cargos':[{'codigo':'MMANIP.','unidades':1,'precio':1.68}]},{'vivace':'2017/22615','bultos':2,'m3':0.099,'kg':18,'transpnif':'A28815017','albaranes':[{'numero':'2017011793','cargos':[{'codigo':'METCIVR2','unidades':3,'precio':0.09},{'codigo':'EMB4VR2','unidades':1,'precio':0.34}]}],'cargos':[{'codigo':'MMANIP.','unidades':1,'precio':1.68}]}]}"

        'Dim sJsonInputString As String = "[{'fecha':'18/09/2017','remite':{'nif':'A62572342','nombre':' VIVACE LOGISTICA'},'destino':{'nif':'A58007857','nombre':'MATIAS MASSO, S.A.'},'expediciones':[{'vivace':'2017/57098','bultos':10,'m3':0.0,'kg':0,'transpnif':'B28905784','albaranes':[{'numero':'2017016229','cargos':null}],'cargos':[{'codigo':'MANIP.','unidades':1,'precio':1.675}]},{'vivace':'2017/57432','bultos':13,'m3':0.0,'kg':0,'transpnif':'','albaranes':[{'numero':'2017016422','cargos':[{'codigo':'METCIVR2','unidades':29,'precio':0.091}]}],'cargos':[{'codigo':'MANIP.','unidades':1,'precio':1.675},{'codigo':'EMB1.3VR2','unidades':3,'precio':0.777},{'codigo':'EMB4.5VR2','unidades':8,'precio':0.237},{'codigo':'EMB2.5VR2','unidades':1,'precio':0.461},{'codigo':'EMB2VR2','unidades':1,'precio':0.551}]},{'vivace':'2017/58956','bultos':13,'m3':0.0,'kg':0,'transpnif':'B28905784','albaranes':[{'numero':'2017016680','cargos':null}],'cargos':[{'codigo':'MANIP.','unidades':1,'precio':1.675}]}]}]"
        'Dim sJsonInputString As String = "{'fecha':'18/09/2017','remite':{'nif':'A62572342','nombre':' VIVACE LOGISTICA'},'destino':{'nif':'A58007857','nombre':'MATIAS MASSO, S.A.'},'expediciones':[{'vivace':'2017/57098','bultos':10,'m3':0.0,'kg':0,'transpnif':'B28905784','albaranes':[{'numero':'2017016229','cargos':null}],'cargos':[{'codigo':'MANIP.','unidades':1,'precio':1.675}]},{'vivace':'2017/57432','bultos':13,'m3':0.0,'kg':0,'transpnif':'','albaranes':[{'numero':'2017016422','cargos':[{'codigo':'METCIVR2','unidades':29,'precio':0.091}]}],'cargos':[{'codigo':'MANIP.','unidades':1,'precio':1.675},{'codigo':'EMB1.3VR2','unidades':3,'precio':0.777},{'codigo':'EMB4.5VR2','unidades':8,'precio':0.237},{'codigo':'EMB2.5VR2','unidades':1,'precio':0.461},{'codigo':'EMB2VR2','unidades':1,'precio':0.551}]},{'vivace':'2017/58956','bultos':13,'m3':0.0,'kg':0,'transpnif':'B28905784','albaranes':[{'numero':'2017016680','cargos':null}],'cargos':[{'codigo':'MANIP.','unidades':1,'precio':1.675}]}]}"
        'Dim sJsonInputString As String = "{'fecha':'09-18-2017','remite':{'nif':'A62572342','nombre':' VIVACE LOGISTICA'},'destino':{'nif':'A58007857','nombre':'MATIAS MASSO, S.A.'},'expediciones':[{'vivace':'2017/57098','bultos':10,'m3':0.0,'kg':0,'transpnif':'B28905784','albaranes':[{'numero':'2017016229','cargos':null}],'cargos':[{'codigo':'MANIP.','unidades':1,'precio':1.675}]},{'vivace':'2017/57432','bultos':13,'m3':0.0,'kg':0,'transpnif':'','albaranes':[{'numero':'2017016422','cargos':[{'codigo':'METCIVR2','unidades':29,'precio':0.091}]}],'cargos':[{'codigo':'MANIP.','unidades':1,'precio':1.675},{'codigo':'EMB1.3VR2','unidades':3,'precio':0.777},{'codigo':'EMB4.5VR2','unidades':8,'precio':0.237},{'codigo':'EMB2.5VR2','unidades':1,'precio':0.461},{'codigo':'EMB2VR2','unidades':1,'precio':0.551}]},{'vivace':'2017/58956','bultos':13,'m3':0.0,'kg':0,'transpnif':'B28905784','albaranes':[{'numero':'2017016680','cargos':null}],'cargos':[{'codigo':'MANIP.','unidades':1,'precio':1.675}]}]}"
        'ISO-8601 date format:
        Dim sb As New System.Text.StringBuilder
        Dim oDlg As New OpenFileDialog
        With oDlg
            If .ShowDialog() Then
                Dim lines As String() = System.IO.File.ReadAllLines(.FileName)
                For Each line As String In lines
                    sb.Append(line)
                Next
            End If
        End With

        'sUrl = "http://localhost:52887/api/vivace/test"
        Dim sJsonInputString As String = sb.ToString '"{'fecha':'2017/09/18','remite':{'nif':'A62572342','nombre':' VIVACE LOGISTICA'},'destino':{'nif':'A58007857','nombre':'MATIAS MASSO, S.A.'},'expediciones':[{'vivace':'2017/57098','bultos':10,'m3':0.0,'kg':0,'transpnif':'B28905784','albaranes':[{'numero':'2017016229','cargos':null}],'cargos':[{'codigo':'MANIP.','unidades':1,'precio':1.675}]},{'vivace':'2017/57432','bultos':13,'m3':0.0,'kg':0,'transpnif':'','albaranes':[{'numero':'2017016422','cargos':[{'codigo':'METCIVR2','unidades':29,'precio':0.091}]}],'cargos':[{'codigo':'MANIP.','unidades':1,'precio':1.675},{'codigo':'EMB1.3VR2','unidades':3,'precio':0.777},{'codigo':'EMB4.5VR2','unidades':8,'precio':0.237},{'codigo':'EMB2.5VR2','unidades':1,'precio':0.461},{'codigo':'EMB2VR2','unidades':1,'precio':0.551}]},{'vivace':'2017/58956','bultos':13,'m3':0.0,'kg':0,'transpnif':'B28905784','albaranes':[{'numero':'2017016680','cargos':null}],'cargos':[{'codigo':'MANIP.','unidades':1,'precio':1.675}]}]}"
        'Dim sJsonInputString As String = "{'nom':'Jack'}"
        Dim jsonOutputString As String = ""
        Dim exs As New Generic.List(Of System.Exception)
        Dim retval = SendRequest(sUrl, sJsonInputString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function SendRequest(url As String, jsonInputString As String, contentType As String, method As String, ByRef jsonOutputString As String, exs As Generic.List(Of System.Exception)) As Boolean
        Dim retval As Boolean
        Dim uri As New System.Uri(url)
        Dim jsonDataBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(jsonInputString)
        Dim req As System.Net.WebRequest = System.Net.WebRequest.Create(uri)
        req.ContentType = contentType
        req.Method = method
        req.ContentLength = jsonDataBytes.Length

        Try
            Dim stream = req.GetRequestStream()
            If jsonDataBytes IsNot Nothing Then
                stream.Write(jsonDataBytes, 0, jsonDataBytes.Length)
            End If
            stream.Close()

            Dim oResponse As System.Net.WebResponse = req.GetResponse()
            Dim oResponseStream = req.GetResponse().GetResponseStream()

            Dim reader As New System.IO.StreamReader(oResponseStream)
            jsonOutputString = reader.ReadToEnd()
            reader.Close()
            oResponseStream.Close()
            retval = True
        Catch ex As System.Exception
            exs.Add(ex)
        End Try

        Return retval
    End Function

End Module
