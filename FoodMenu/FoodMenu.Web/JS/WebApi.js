
var webApi = {};
webApi = function () {
    var self = {};

    self.apiConnectionData = function () {
        var cookie = Cookies.get('token');

        return {
            userId: cookie.split('~')[0],
            accessToken: cookie.split('~')[1]
        };
    }

    self.getHeaders = function () {
        api = self.apiConnectionData();
        return {
            'userId': api.userId,
            'token': api.accessToken,
            'content-type': 'application/json'
        };
    }

    self.ajax = function (url, options) {
        // var baseUrl = "http://localhost:52746/";
        var api = options.allowAnon ? { userId: 0, accessToken: '' } : self.apiConnectionData();//base api
        var headers = {
            'userId': api.userId,
            'token': api.accessToken,
            'content-type': 'application/json'
        };

        var endOptions = $.extend({}, options, {
            url: common.baseURl + url,
            headers: headers

        });

        return $.ajax(endOptions);

    }

    // ajax().done(function (privData) { })
    //type: "POST",
    //data: ko.toJSON(quotamodel),
    //cache: false,
    //beforeSend: clickd.showLoader


    return self;
}();