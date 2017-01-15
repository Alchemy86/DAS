$('[data-countdown]').each(function () {
    var $this = $(this), finalDate = moment.tz($(this).data('countdown'), "PST8PDT").toDate();
    $this.countdown(finalDate, function (event) {
        if (event.strftime('%D') > 0) {
            $this.html(event.strftime('%D days %H:%M:%S'));
        } else if (event.strftime('%H') > 0) {
            $this.html(event.strftime('%H Hrs %M Min %S Sec'));
        } else if (event.strftime('%M') > 0) {
            $this.html(event.strftime('%M Min %S Sec'));
        } else if (event.strftime('%S') >= 0) {
            $this.html(event.strftime('%S Sec'));
        }
    }).on('update.countdown', function (event) {
        if (event.elapsed) {
            $this.html('Ended');
        }
    });;
});