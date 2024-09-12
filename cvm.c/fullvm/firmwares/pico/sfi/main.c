#include <stdio.h>
#include "sfi.h"
#include "pico/stdlib.h"
#include "hardware/spi.h"
#include "hardware/i2c.h"
#include "hardware/interp.h"
#include "hardware/timer.h"
#include "hardware/clocks.h"
int main()
{
    size_t ReciverBufSize = SSD1306_HEIGHT * SSD1306_WIDTH * 4 * sizeof(char);
    stdio_init_all();
    bi_decl(bi_2pins_with_func(PICO_DEFAULT_I2C_SDA_PIN, PICO_DEFAULT_I2C_SCL_PIN, GPIO_FUNC_I2C));
    bi_decl(bi_program_description("Simple Firmware Interface RP2040/2350"));
    i2c_init(i2c_default, SSD1306_I2C_CLK * 1000);
    gpio_set_function(PICO_DEFAULT_I2C_SDA_PIN, GPIO_FUNC_I2C);
    gpio_set_function(PICO_DEFAULT_I2C_SCL_PIN, GPIO_FUNC_I2C);
    gpio_pull_up(PICO_DEFAULT_I2C_SDA_PIN);
    gpio_pull_up(PICO_DEFAULT_I2C_SCL_PIN);
    SFIInfo info;
    SFIDevice devices[2];
    devices[0].Type=DEVICE_TYPE_STORAGE;
    devices[0].Location=0;
    devices[0].Name="SD CARD";
    info.ProtocolMainVersion = 0;
    info.ProtocolMinVersion = 1;
    info.Name = "RP2040/2350";
    SSD1306_init();
    char protocol[32];
    char WriteBuf[512];
    char ReadBuf[512];
    SFIIOOperation io_info;
    while (1)
    {
        // scanf("%s", protocol);
        char Operator = uart_getc(uart0);
        switch (Operator)
        {
        case 'I':
            uart_write_blocking(uart0, &info, sizeof(SFIInfo));
            break;
        case 'W':
        {
            uart_read_blocking(uart0, &io_info, sizeof(io_info));
            // Actual Write
            uart_read_blocking(uart0, &WriteBuf, io_info.Length);
        }
        break;
        case 'R':
        {
            uart_read_blocking(uart0, &io_info, sizeof(io_info));
            // Actual Read
            uart_write_blocking(uart0, &ReadBuf, io_info.Length);
        }
        break;
        default:
            break;
        }
    }
}