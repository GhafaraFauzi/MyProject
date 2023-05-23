$(document).ready(function () {
    $('#login-form').submit(function (e) {
        e.preventDefault();
        var Account = new Object();
        Account.Email = $('#Email').val();
        Account.Password = $('#Password').val();
        debugger;
        $.ajax({
            type: 'POST',
            url: 'http://localhost:8082/api/Account/Login',
            data: JSON.stringify(Account), //convert json
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                debugger;

                //JWT TOKEN
                //var jwt = new JsonWebToken();
                //var token = jwt.GenerateJwtToken(Account.Email);

                $.post("/Home/Login", { email: Account.Email })
                    .done(function () {
                        var getToken = result.token
                        sessionStorage.setItem("token", getToken) //simpan token pada session storage
                        console.log(getToken)
                        Swal.fire({
                            icon: 'success',
                            title: result.message,
                            showConfirmButton: false,
                            timer: 2500
                        }).then((successAllert) => {
                            if (successAllert) {
                                location.replace("/Departemens/Index");
                            } else {
                                location.replace("/Departemens/Index");
                            }
                        });
                    })
                    .fail(function () {
                        alert("Fail!, Gagal Login");
                    })
                    .always(function () {
                        //alert
                    });
            },
            error: function (errorMessage) {
                Swal.fire('Password Salah', errorMessage.message, 'error');
            }
        });
    })
});

$("#logout").on("click", function () {
    sessionStorage.removeItem("token")
    location.replace("/Home/Login")
})