import { RestaurnatsAppPage } from './app.po';

describe('restaurnats-app App', () => {
  let page: RestaurnatsAppPage;

  beforeEach(() => {
    page = new RestaurnatsAppPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
