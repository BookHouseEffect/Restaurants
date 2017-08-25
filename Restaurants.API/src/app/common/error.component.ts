export abstract class ErrorComponent {
    success: Array<string> = new Array<string>(0);
    error: Array<string> = new Array<string>(0);

    clearSuccess() {
        while (this.success.length > 0)
            this.success.pop();
    };

    clearError() {
        while (this.error.length > 0)
            this.error.pop();
    }

}