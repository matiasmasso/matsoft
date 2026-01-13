@ModelType String

<body onload="autodial()">
        <a id="callme" href="tel:@(Model)" tcxhref="@(Model)" ></a>
 
    <script>
        function autodial() {
            document.getElementById('callme').click();
            window.close();
        }
    </script>

</body>