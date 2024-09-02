#if !defined(__MINIAL_POSIX)
#define __MINIAL_POSIX
#ifdef _WIN32
void write(FILE* F, void *buf, size_t size);
#endif

#endif // __MINIAL_POSIX
