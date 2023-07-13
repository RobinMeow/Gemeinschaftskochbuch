export const LOG_SUCCESS: (text: string) => void = (text: string) => {
    console.log('%c'+text, 'color: #8df060;');
};

export const LOG_FAILURE: (text: string) => void = (text: string) => {
    console.log('%c'+text, 'color: #f53333;');
};
